using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool IsGamePaused { get; private set; }
    
    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private Spawner[] _spawnerList;
    [SerializeField] private GameObject _pauseMenu;
    
    private Coroutine _fadeScreenCoroutine;
    private bool _canBePaused = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU_SCENE)
        {
            GameStateManager.InitGameState();
        }
        else if (SceneManager.GetActiveScene().name == SceneNames.KITOMIR_HOME_SCENE)
        {
            GameStateManager.State = GameState.AtHome;
        }

        if (SceneManager.GetActiveScene().name == SceneNames.CORRIDOR_SCENE)
        {
            foreach (var spawner in _spawnerList)
            {
                if (spawner.Scene.name == GameStateManager.PreviousSceneName)
                {
                    Player.Instance.gameObject.transform.position = spawner.transform.position;
                }
            }
        }
        
        Door.OnDoorOpen += Door_OnDoorOpen;
        MenuButton.OnPlayButtonPressed += MenuButton_OnPlayButtonPressed;
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        InputManager.Instance.OnSecretInputSolved += InputManager_OnSecretInputSolved;
        
        _pauseMenu.SetActive(false);
        _fadeScreenCoroutine = StartCoroutine(_fadeScreen.Appear(1.5f));
    }

    private void InputManager_OnSecretInputSolved(object sender, EventArgs e)
    {
        StartCoroutine(LoadScene(SceneNames.SECRET_GAME_MODE_SCENE));
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e)
    {
        if (SceneManager.GetActiveScene().name != SceneNames.MAIN_MENU_SCENE)
        {
            Pause();
        }
    }

    private void MenuButton_OnPlayButtonPressed(object sender, EventArgs e)
    {
        StartCoroutine(LoadScene(SceneNames.KITOMIR_HOME_SCENE));
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        if (Player.Instance != null)
        {
            Player.Instance.CanAct = false;
        }
        StartCoroutine(LoadScene(e.SceneToLoadName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        _canBePaused = false;
        float waitAfterFadingDuration = 0f;
        
        // if (GameStateManager.State == GameState.AtHome)
        // {
        //     waitAfterFadingDuration = 13f;
        //     GameStateManager.State = GameState.PhysicsExam;
        // }
        // else if (GameStateManager.State == GameState.ITExam)
        // {
        //     waitAfterFadingDuration = 13f;
        //     GameStateManager.State = GameState.ExamsPassed;
        // }
        GameStateManager.PreviousSceneName = SceneManager.GetActiveScene().name;
        
        StopCoroutine(_fadeScreenCoroutine);
        yield return StartCoroutine(_fadeScreen.Fade(1.5f, waitAfterFadingDuration));

        SceneManager.LoadScene(sceneName);
    }

    private void Pause()
    {
        if (_canBePaused)
        {
            if (!IsGamePaused)
            {
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true); 
                IsGamePaused = true;
                
                if (Player.Instance != null)
                {
                    Player.Instance.CanAct = false;
                }
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;if (Player.Instance != null)
        {
            Player.Instance.CanAct = true;
        }
        IsGamePaused = false;
    }
    
    private void OnDisable()
    {
        Door.OnDoorOpen -= Door_OnDoorOpen;
        MenuButton.OnPlayButtonPressed -= MenuButton_OnPlayButtonPressed;
        InputManager.Instance.OnPauseAction -= InputManager_OnPauseAction;
    }

    public static void TimeScaleZeroInvoke(object sender, EventArgs e, EventHandler eventToInvoke)
    {
        Time.timeScale = 1f;
        eventToInvoke?.Invoke(sender, e);
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
        }
    }

    public static void TimeScaleZeroInvoke(UnityEvent unityEvent)
    {
        Time.timeScale = 1f;
        unityEvent?.Invoke();
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
        }
    }

    public static void RestartGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        if (Timer.Instance != null)
        {
            Destroy(Timer.Instance.gameObject);
        }
        SceneManager.LoadScene(SceneNames.MAIN_MENU_SCENE);
    }
}
