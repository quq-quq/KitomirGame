using System;

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
            OnStateChanged?.Invoke(null, new OnStateChangedEventArgs { CurrentState = _state });
        }
    }

    public class OnStateChangedEventArgs : EventArgs
    {
        public GameState CurrentState;
    }


    private static GameState _state;

    public static void InitGameState()
    {
        State = GameState.MainMenu;
    }
}

public enum GameState
{
    MainMenu,
    AtHome,
    PhysicsExam,
    MathsExam,
    ITExam,
    ExamsFailed,
    ExamsPassed,
}
