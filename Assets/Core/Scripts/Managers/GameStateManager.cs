
public static class GameStateManager
{
    public static GameState State;
    public static string PreviousSceneName;

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
