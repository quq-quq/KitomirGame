using System;
using UnityEngine;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _restartPanelCanvas;

    private void Start()
    {
        SecretGameModePlayer.Instance.OnGameOver += Player_OnGameOver;
    }

    private void Player_OnGameOver(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        _restartPanelCanvas.SetActive(true);
    }

}
