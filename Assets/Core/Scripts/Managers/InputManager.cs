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
    public event EventHandler OnSecretInputSolved;


    private const string MAIN_MENU_SECRET_INPUT = "rulada";
    
    private PlayerInputActions _playerInputActions;
    private MenuInputActions _menuInputActions;
    private string _mainMenuPlayerSecretInput;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("changed instance field");
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == SceneInfo.MAIN_MENU_SCENE 
            && _mainMenuPlayerSecretInput != MAIN_MENU_SECRET_INPUT
            && GameStateManager.State != GameState.FirstEntry)
        {
            if (Input.anyKeyDown)
            {
                _mainMenuPlayerSecretInput += Input.inputString.ToLower();
                if (Input.inputString.ToLower() == "r")
                {
                    _mainMenuPlayerSecretInput = "r";
                }

                if (_mainMenuPlayerSecretInput.Length > 7)
                {
                    _mainMenuPlayerSecretInput = "";
                }
            }
            
            if (_mainMenuPlayerSecretInput == MAIN_MENU_SECRET_INPUT)
            {
                OnSecretInputSolved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void PressButton_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuButtonPressed);
        }
        else
        {
            OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DownSwitch_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuSwitchDown);
        }
        else
        {
            OnMenuSwitchDown?.Invoke(this, EventArgs.Empty);
        }

        // if (OnMenuSwitchDown != null)
        // {
        //     Debug.Log("subscribers:");
        //     foreach (Delegate subscriber in OnMenuSwitchDown.GetInvocationList())
        //     {
        //         Debug.Log(subscriber.Method.Name);
        //     }
        // }
        // else
        // {
        //     Debug.Log("null fuck ya");
        // }
    }

    private void UpSwitch_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(this, EventArgs.Empty, OnMenuSwitchUp);
        }
        else
        {
            OnMenuSwitchUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVector()
    {
        Vector2 movementVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return movementVector;
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
