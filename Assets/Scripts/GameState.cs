
public class GameState
{
    private static Difficulty GameDifficulty = Difficulty.Normal;
    private static int Lives = 3;
    private static int Score = 0;

    public static void NewState(Difficulty difficulty)
    {
        GameDifficulty = difficulty;
        Lives = 3;
        Score = 0;
    }

    public static int GetLives()
    {
        return Lives;
    }

    public static void SetLives(int lives)
    {
        Lives = lives;
    }

    public static int GetScore()
    {
        return Score;
    }

    public static void AddScore(int score)
    {
        Score += score;
    }

    public static Difficulty GetDifficulty()
    {
        return GameDifficulty;
    }
}
