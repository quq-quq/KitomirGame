using System;
using UnityEngine;

public static class GameStateManager
{
    public static event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public static string PreviousSceneName;
    public static GameState State
    {
        get => _state;
        set
        {
            _state = value;
            Debug.Log(State);
            OnStateChanged?.Invoke(null, new OnStateChangedEventArgs { CurrentState = _state });
        }
    }

    public class OnStateChangedEventArgs : EventArgs
    {
        public GameState CurrentState;
    }


    private static GameState _state;
}

public enum GameState
{
    FirstEntry,
    MainMenu,
    AtHome,
    PhysicsExam,
    MathsExam,
    ITExam,
    ExamsFailed,
    ExamsPassed,
}
