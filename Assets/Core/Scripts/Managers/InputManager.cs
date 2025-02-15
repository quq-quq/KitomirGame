using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnMainMenuSwitchUp;
    public event EventHandler OnMainMenuSwitchDown;
    public event EventHandler OnMainMenuButtonPressed;
    
    private PlayerInputActions _playerInputActions;
    private MainMenuInputActions _menuInputActions;
    
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

        if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            _menuInputActions = new MainMenuInputActions();
            _menuInputActions.Enable();
            _menuInputActions.Navigation.UpSwitch.performed += UpSwitch_performed;
            _menuInputActions.Navigation.DownSwitch.performed += DownSwitch_performed;
            _menuInputActions.Navigation.PressButton.performed += PressButton_performed;
        }
    }

    private void PressButton_performed(InputAction.CallbackContext obj)
    {
        OnMainMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void DownSwitch_performed(InputAction.CallbackContext obj)
    {
        OnMainMenuSwitchDown?.Invoke(this, EventArgs.Empty);
    }

    private void UpSwitch_performed(InputAction.CallbackContext obj)
    {
        OnMainMenuSwitchUp?.Invoke(this, EventArgs.Empty);
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

    private void OnDisable()
    {
        _playerInputActions.Player.Interact.performed -= Interact_performed;
        _playerInputActions.Player.Disable();

        if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            _menuInputActions.Navigation.UpSwitch.performed -= UpSwitch_performed;
            _menuInputActions.Navigation.DownSwitch.performed -= DownSwitch_performed;
            _menuInputActions.Navigation.PressButton.performed -= PressButton_performed;
            _menuInputActions.Disable();
        }
    }
}
