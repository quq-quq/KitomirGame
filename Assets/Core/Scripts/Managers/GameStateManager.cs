
public static class GameStateManager
{
    public static GameState State;
    public static string PreviousSceneName;

    public static void InitGameState()
    {
        State = GameState.AtHome;
    }
}

public enum GameState
{
    AtHome,
    PhysicsExam,
    MathsExam,
    ITExam,
    ExamsFailed,
    ExamsPassed,
}
