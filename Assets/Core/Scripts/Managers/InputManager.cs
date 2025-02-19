using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnMenuSwitchUp;
    public event EventHandler OnMenuSwitchDown;
    public event EventHandler OnMenuButtonPressed;
    
    private PlayerInputActions _playerInputActions;
    private MenuInputActions _menuInputActions;
    
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
        _playerInputActions.Player.Pause.performed += Pause_performed;
        
        _menuInputActions = new MenuInputActions();
        _menuInputActions.Enable();
        _menuInputActions.Navigation.UpSwitch.performed += UpSwitch_performed;
        _menuInputActions.Navigation.DownSwitch.performed += DownSwitch_performed;
        _menuInputActions.Navigation.PressButton.performed += PressButton_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void PressButton_performed(InputAction.CallbackContext obj)
    {
        // we invoke this event only when the game is paused or when we are in the main menu scene
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuButtonPressed);
        }
        else if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void DownSwitch_performed(InputAction.CallbackContext obj)
    {
        // we invoke this event only when the game is paused or when we are in the main menu scene
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuSwitchDown);
        }
        else if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            OnMenuSwitchDown?.Invoke(this, EventArgs.Empty);
        }
    }

    private void UpSwitch_performed(InputAction.CallbackContext obj)
    {
        // we invoke this event only when the game is paused or when we are in the main menu scene
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuSwitchUp);
        }
        else if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            OnMenuSwitchUp?.Invoke(this, EventArgs.Empty);
        }
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

        _menuInputActions.Navigation.UpSwitch.performed -= UpSwitch_performed;
        _menuInputActions.Navigation.DownSwitch.performed -= DownSwitch_performed;
        _menuInputActions.Navigation.PressButton.performed -= PressButton_performed;
        _menuInputActions.Disable();
    }
}
