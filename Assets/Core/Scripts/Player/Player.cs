using System;
using UnityEngine;


public class Player : MonoBehaviour 
{
    public static Player Instance { get; private set; }
    public bool IsMoving { get; private set; }
    public bool CanAct { get; set; } = true;

    [SerializeField] private float _moveSpeed = 5f;
    
    private Vector2 _movementVector;
    private Rigidbody2D _rigidbody2D;
    private InteractableObject _selectedObject;
    private PlayerVisual _playerVisual;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerVisual = GetComponentInChildren<PlayerVisual>();
    }

    private void FixedUpdate()
    {
        if (DialogueViewer.IsGoing)
        {
            CanAct = false;
        }
        HandleMovement();
        _playerVisual.UpdateVisual(_movementVector.x, _movementVector.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactableObject))
        {
            if (interactableObject.TrySelect())
            {
                _selectedObject = interactableObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactableObject))
        {
            if (_selectedObject == interactableObject)
            {
                _selectedObject.Deselect();
                _selectedObject = null;
            }
        }
    }

    private void HandleMovement()
    {
        if (CanAct)
        {
            _movementVector = InputManager.Instance.GetMovementVectorNormalised();
            _rigidbody2D.linearVelocity = _movementVector * _moveSpeed;
            IsMoving = _movementVector != Vector2.zero;
        }
        else if (IsMoving)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            IsMoving = false;
        }
    }

    private void InputManager_OnInteractAction(object sender, EventArgs e)
    {
        if (_selectedObject != null && CanAct)
        {
            _selectedObject.Interact();
        }
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteractAction -= InputManager_OnInteractAction;
    }
}
