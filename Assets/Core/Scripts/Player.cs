using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public static Player Instance { get; private set; }
    
    public event EventHandler<OnDoorOpenEventArgs> OnDoorOpened;

    

    public class OnDoorOpenEventArgs : EventArgs {
        public Transform senderTransform;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator _anim;

    private Rigidbody2D _rb;
    private InteractiveObject _selectedInteractiveObject;
    private bool _isWalking;
    private Vector2 _inputVector;
    private float _x;
    private float _y;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetMovementInput();
        Animate();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Movement() 
    {
        _rb.linearVelocity = _inputVector * moveSpeed;
    }

    private void GetMovementInput()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
        _inputVector = new Vector2(_x, _y);
        _inputVector.Normalize();
    }

    private void Animate()
    {
        if (_inputVector.magnitude != 0f)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }

        if (_isWalking)
        {
            _anim.SetFloat("x", _x);
            _anim.SetFloat("y", _y);
        }
        _anim.SetBool("moving", _isWalking);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (_selectedInteractiveObject != null)
        {
            _selectedInteractiveObject.Interact();
            if (_selectedInteractiveObject is Door)
            {
                OnDoorOpened?.Invoke(this, new OnDoorOpenEventArgs
                {
                    senderTransform = _selectedInteractiveObject.transform
                }
                );
            }
        }
    }

    public void SelectInteractiveObject(InteractiveObject interactiveObject) {
        if (_selectedInteractiveObject is null) {
            _selectedInteractiveObject = interactiveObject;
        }
    }

    public void DeselectInteractiveObject(InteractiveObject interactiveObject) {
        if (_selectedInteractiveObject == interactiveObject) {
            _selectedInteractiveObject = null;
        }
    }

    public bool IsWalking() {
        return _isWalking;
    }
}
