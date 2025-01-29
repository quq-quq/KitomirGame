using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public event EventHandler OnInteractAction;
    
    private PlayerInputActions _playerInputActions;
    
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
        
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += Interact_performed;
    }

    public Vector2 GetMovementVectorNormalised()
    {
        Vector2 movementVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        return movementVector.normalized;
    }
    
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
}
