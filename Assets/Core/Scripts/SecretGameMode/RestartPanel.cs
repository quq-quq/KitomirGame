using System;
using UnityEngine;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _restartPanelCanvas;
    public static event EventHandler OnRestartPanelOpened;

    private void Start()
    {
        SecretGameModePlayer.Instance.OnGameOver += Player_OnGameOver;
    }

    private void Player_OnGameOver(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        _restartPanelCanvas.SetActive(true);
        OnRestartPanelOpened?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        SecretGameModePlayer.Instance.OnGameOver -= Player_OnGameOver;
    }
}
