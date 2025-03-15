using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public event EventHandler OnAlmostOutOfTime;
    public bool IsRunning { get; private set; }
    
    [SerializeField] private TimerVisual _timerVisual;
    
    private const float TIMER_MAX = 15f * 60; //15 minutes
    private const float ALMOST_OUT_OF_TIME = 10f;
    
    private float _timer; 
    private bool _isAlmostOutOfTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (_timer > 0 && IsRunning)
        {
            _timer -= Time.deltaTime;
            _timerVisual.UpdateVisual(_timer/TIMER_MAX, _timer);
            if (_timer <= ALMOST_OUT_OF_TIME && !_isAlmostOutOfTime)
            {
                OnAlmostOutOfTime?.Invoke(this, EventArgs.Empty);
                _isAlmostOutOfTime = true;
            }
        }
        else if (_timer <= 0 && IsRunning)
        {
            IsRunning = false;
            GameStateManager.State = GameState.ExamsFailed;
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
        IsRunning = true;
    }

    public void PauseTimer()
    {
        IsRunning = false;
        gameObject.SetActive(false);
    }

    public void ResumeTimer()
    {
        IsRunning = true;
        gameObject.SetActive(true);
    }
    
}
