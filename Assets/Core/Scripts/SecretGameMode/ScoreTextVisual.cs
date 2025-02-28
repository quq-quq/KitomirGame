using System;
using TMPro;
using UnityEngine;

public class ScoreTextVisual : MonoBehaviour
{
    private const string DEFAULT_TEXT = "счёт: ";
    private TextMeshProUGUI _text;
    
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        SecretGameModePlayer.Instance.OnScoreChanged += Player_OnScoreChanged;
    }

    private void Player_OnScoreChanged(object sender, EventArgs e)
    {
        _text.text = DEFAULT_TEXT + SecretGameModePlayer.Instance.Score;
    }
}
