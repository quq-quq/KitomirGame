using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _progressBar;

    private const float DEFAULT_PROGRESS_BAR_WIDTH = 1720;
    private const float DEFAULT_PROGRESS_BAR_HEIGHT = 50;
    private readonly Color _startProgressBarColor = new Color(0.15f, 0.58f, 0.17f);
    private readonly Color _endProgressBarColor = new Color(0.65f, 0.11f, 0.11f);


    public void UpdateVisual(float percent, float timeLeft)
    {
        _progressBar.rectTransform.sizeDelta = new Vector2(DEFAULT_PROGRESS_BAR_WIDTH * percent, DEFAULT_PROGRESS_BAR_HEIGHT);
        _timerText.text = GetTimerText(timeLeft);
        
        _progressBar.color = Color.Lerp(_endProgressBarColor, _startProgressBarColor, percent);
        
    }

    private string GetTimerText(float timeLeft)
    {
        return $"{(int)(timeLeft / 60)}:{(int)(timeLeft % 60):00}";
    }
}
