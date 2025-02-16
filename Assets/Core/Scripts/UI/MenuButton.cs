using System;
using UnityEngine;
using UnityEngine.Events;


public class MenuButton : MonoBehaviour
{
    [SerializeField] public UnityEvent onPressMethod;

    public static event EventHandler OnPlayButtonPressed; 
    
    public void Play()
    {
        OnPlayButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
