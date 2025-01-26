using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    
    public static Player Instance { get; private set; }
    
    public event EventHandler<OnDoorOpenEventArgs> OnDoorOpened;
    
    
    public class OnDoorOpenEventArgs : EventArgs {
        public Transform SenderTransform;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator anim;

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
        transform.position = ConfigGameManager.Instance.spawnAtOnNewScene;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        FadeScreen.Instance.OnFadeStarted += FadeScreen_OnFadeStarted;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FadeScreen_OnFadeStarted(object sender, FadeScreen.OnFadeStartedEventArgs e) {
        moveSpeed = 0f;
    }

    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVectorNormalised();
        _x = _inputVector.x;
        _y = _inputVector.y;
        Animate();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Movement() 
    {
        _rb.linearVelocity = _inputVector * moveSpeed;
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
            anim.SetFloat("x", _x);
            anim.SetFloat("y", _y);
        }
        anim.SetBool("moving", _isWalking);
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
                    SenderTransform = _selectedInteractiveObject.transform
                });
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

    public bool IsItemSelected(InteractiveObject interactiveObject) {
        return _selectedInteractiveObject == interactiveObject;
    }
}
