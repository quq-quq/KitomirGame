using System;
using UnityEngine;
using UnityEngine.Events;


public class MenuButton : MonoBehaviour
{
    [SerializeField] public UnityEvent OnPressMethod;

    public static event EventHandler OnPlayButtonPressed;
    public static event EventHandler OnHiddenButtonsNeedToSetActive;
    
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
