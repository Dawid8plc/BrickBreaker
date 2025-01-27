
public class ScoreEntry
{
    public string Name = string.Empty;

    public int Score = 0;

    public ScoreEntry() { }

    public ScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public void SetScore(int score)
    {
        Score = score;
    }

    public int GetScore()
    {
        return Score;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }
}
