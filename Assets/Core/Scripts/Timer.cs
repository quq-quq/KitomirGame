using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public event EventHandler OnAlmostOutOfTime;
    
    [SerializeField] private TimerVisual _timerVisual;
    
    private const float TIMER_MAX = 15f; //15 minutes
    private const float ALMOST_OUT_OF_TIME = 10f;
    
    private float _timer; 
    private bool _isRunning;
    private bool _isAlmostOutOfTime;

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
            if (_timer <= ALMOST_OUT_OF_TIME && !_isAlmostOutOfTime)
            {
                OnAlmostOutOfTime?.Invoke(this, EventArgs.Empty);
                _isAlmostOutOfTime = true;
            }
        }
        else if (_timer <= 0 && _isRunning)
        {
            GameStateManager.State = GameState.ExamsFailed;
            _isRunning = false;
        }
    }

    public void DestroyTimer()
    {
        Instance = null;
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
