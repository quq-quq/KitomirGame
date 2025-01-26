using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    
    public static GameInput Instance {get; private set;}
    public event EventHandler OnInteractAction;
    
    
    private PlayerInputActions _playerInputActions;
    
    private void Awake() {
        Instance = this;
        
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Interact.performed += Interact_performed;
    }

    public Vector2 GetMovementVectorNormalised() {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        return inputVector.normalized;
    }
    
    private void Interact_performed(InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
}
