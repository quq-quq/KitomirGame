using System;
using System.Collections;
using DG.Tweening;
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
    [SerializeField] private Transform _soliderSpawner;
    [SerializeField] private GameObject _soliderPrefab;
    [SerializeField] private float _soliderFadeDuration = .5f;
    
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
        // change state depending on current scene to easier test inside editor
        // for example when starting not from main menu scene
        if (GameStateManager.State == GameState.FirstEntry 
            && SceneManager.GetActiveScene().name != SceneInfo.MAIN_MENU_SCENE)
        {
            GameStateManager.State = SceneInfo.SceneStates[SceneManager.GetActiveScene().name];
        }

        if (SceneManager.GetActiveScene().name == SceneInfo.CORRIDOR_SCENE)
        {
            foreach (var spawner in _spawnerList)
            {
                if (spawner.Scene.name == GameStateManager.PreviousSceneName)
                {
                    Player.Instance.gameObject.transform.position = spawner.transform.position;
                }
            }
        }

        if (GameStateManager.State == GameState.ExamsPassed)
        {
            Timer.Instance.DestroyTimer();
        }

        Door.OnDoorOpen += Door_OnDoorOpen;
        MenuButton.OnPlayButtonPressed += MenuButton_OnPlayButtonPressed;
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        InputManager.Instance.OnSecretInputSolved += InputManager_OnSecretInputSolved;
        GameStateManager.OnStateChanged += GameStateManager_OnStateChanged;

        if (SceneManager.GetActiveScene().name == SceneInfo.MAIN_MENU_SCENE)
        {
            StartForMenu.OnMenuButtonContainerAppear += StartForMenu_OnMenuButtonContainerAppear;
        }

        _pauseMenu.SetActive(false);
        _fadeScreenCoroutine = StartCoroutine(_fadeScreen.Appear(1.5f));
    }

    private void StartForMenu_OnMenuButtonContainerAppear(object sender, EventArgs e)
    {
        GameStateManager.State = GameState.MainMenu;
    }

    private void InputManager_OnSecretInputSolved(object sender, EventArgs e)
    {
        StartCoroutine(LoadScene(SceneInfo.SECRET_GAME_MODE_SCENE));
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e)
    {
        if (SceneManager.GetActiveScene().name != SceneInfo.MAIN_MENU_SCENE)
        {
            Pause();
        }
    }

    private void MenuButton_OnPlayButtonPressed(object sender, EventArgs e)
    {
        StartCoroutine(LoadScene(SceneInfo.KITOMIR_HOME_SCENE));
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

        if (sceneName != SceneInfo.CORRIDOR_SCENE && GameStateManager.State != SceneInfo.SceneStates[sceneName])
        {
            GameStateManager.State = SceneInfo.SceneStates[sceneName];
        }
        
        if (GameStateManager.State == GameState.AtHome && sceneName == SceneInfo.CORRIDOR_SCENE)
        {
            waitAfterFadingDuration = 13f;
            GameStateManager.State = GameState.PhysicsExam;
        }
        else if (GameStateManager.State == GameState.ExamsPassed && sceneName == SceneInfo.HAPPY_END_SCENE)
        {
            waitAfterFadingDuration = 13f;
        }

        GameStateManager.PreviousSceneName = SceneManager.GetActiveScene().name;

        StopCoroutine(_fadeScreenCoroutine);
        yield return StartCoroutine(_fadeScreen.Fade(1.5f, waitAfterFadingDuration));

        SceneManager.LoadScene(sceneName);
    }

    private void Pause()
    {
        if (_canBePaused && !DialogueViewer.IsGoing)
        {
            if (!IsGamePaused)
            {
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                IsGamePaused = true;
                if (SceneManager.GetActiveScene().name == SceneInfo.HAPPY_END_SCENE)
                {
                    MusicManager.Instance.PauseSoundtrack();
                }

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
        Time.timeScale = 1f;
        if (Player.Instance != null)
        {
            Player.Instance.CanAct = true;
        }

        IsGamePaused = false;
        if (SceneManager.GetActiveScene().name == SceneInfo.HAPPY_END_SCENE)
        {
            MusicManager.Instance.ResumeSoundtrack();
        }
    }

    private void OnDisable()
    {
        Door.OnDoorOpen -= Door_OnDoorOpen;
        MenuButton.OnPlayButtonPressed -= MenuButton_OnPlayButtonPressed;
        InputManager.Instance.OnPauseAction -= InputManager_OnPauseAction;
        InputManager.Instance.OnSecretInputSolved -= InputManager_OnSecretInputSolved;
        GameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;

        if (SceneManager.GetActiveScene().name == SceneInfo.MAIN_MENU_SCENE)
        {
            StartForMenu.OnMenuButtonContainerAppear -= StartForMenu_OnMenuButtonContainerAppear;
        }
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.OnStateChangedEventArgs e)
    {
        if (e.CurrentState == GameState.ExamsFailed)
        {
            if (DialogueViewer.IsGoing)
            {
                //todo finish dialogue
            }

            if (Timer.Instance.IsRunning)
            {
                StartCoroutine(SadSceneTransition());
            }
            else
            {
                StartCoroutine(LoadScene(SceneInfo.SAD_END_SCENE));
            }
        }

        if (e.CurrentState == GameState.ExamsPassed)
        {
            if (Timer.Instance != null)
            {
                Timer.Instance.DestroyTimer();
            }
            PlayerPrefs.SetInt("IsGameCompleted", 1);
        }
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
            Timer.Instance.DestroyTimer();
        }
        GameStateManager.State = GameState.AtHome;

        SceneManager.LoadScene(SceneInfo.MAIN_MENU_SCENE);
    }

    private IEnumerator SadSceneTransition()
    {
        Player.Instance.CanAct = false;
        Timer.Instance.DestroyTimer();
        yield return new WaitForSeconds(1);
        GameObject solider = Instantiate(_soliderPrefab, _soliderSpawner);
        solider.GetComponent<SpriteRenderer>().DOFade(1f, _soliderFadeDuration);
        yield return new WaitForSeconds(2);
        StartCoroutine(LoadScene(SceneInfo.SAD_END_SCENE));
    }
}
