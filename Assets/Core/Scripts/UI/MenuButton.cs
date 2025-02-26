using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class MenuButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textChamber;
    [SerializeField] public UnityEvent OnPressMethod;

    public static event EventHandler OnPlayButtonPressed;
    public static event EventHandler OnHiddenButtonsNeedToSetActive;

    public TextMeshProUGUI TextChamber
    {
        get => _textChamber;
    }

    public void Play()
    {
        OnPlayButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager.Instance.Resume();
    }

    public void BackToMenu()
    {
        OnHiddenButtonsNeedToSetActive?.Invoke(this, EventArgs.Empty);
    }

    public void AcceptBackToMenu()
    {
        GameManager.RestartGame();
    }

    public void RejectBackToMenu()
    {
        OnHiddenButtonsNeedToSetActive?.Invoke(this, EventArgs.Empty);
    }
}
