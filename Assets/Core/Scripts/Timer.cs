using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TimerVisual _timerVisual;
    
    private const float TIMER_MAX = 15 * 60; //15 minutes
    
    public static Timer Instance { get; private set; }
    private float _timer; 
    private bool _isRunning;

    private void Awake()
    {
        if (Instance == null)
        {
            gameObject.SetActive(false);
            Instance = this;
            DontDestroyOnLoad(this);
            
            // 
            StartTimer();
            //
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        if (_timer > 0 && _isRunning)
        {
            _timer -= Time.deltaTime;
            _timerVisual.UpdateVisual(_timer/TIMER_MAX, _timer);
        }
        else if (_timer <= 0 && _isRunning)
        {
            GameStateManager.State = GameState.ExamsFailed;
        }
    }

    public void DestroyTimer()
    {
        Destroy(gameObject);
    }

    public void StartTimer()
    {
        _timer = TIMER_MAX;
        gameObject.SetActive(true);
        _isRunning = true;
    }

    public void PauseTimer()
    {
        _isRunning = false;
        gameObject.SetActive(false);
    }

    public void ResumeTimer()
    {
        _isRunning = true;
        gameObject.SetActive(true);
    }
    
}
