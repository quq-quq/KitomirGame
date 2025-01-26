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

    
    private InteractiveObject _selectedInteractiveObject;
    private bool _isWalking;

    private void Awake() {
        Instance = this; 
    }

    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (_selectedInteractiveObject != null) {
            _selectedInteractiveObject.Interact();
            if (_selectedInteractiveObject is Door) {
                OnDoorOpened?.Invoke(this, new OnDoorOpenEventArgs {
                        senderTransform = _selectedInteractiveObject.transform
                    }
                );
            }
        }
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0);

        if (moveDirection != Vector3.zero) {
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            _isWalking = true;
        }
        else {
            _isWalking = false;
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
