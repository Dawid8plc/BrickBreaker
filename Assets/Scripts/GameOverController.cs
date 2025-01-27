using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] TMP_Text GameOverReason;
    [SerializeField] TMP_Text FinalScore;
    [SerializeField] TMP_Text FinalTime;
    [SerializeField] TMP_InputField NameField;

    public void Setup(int reason)
    {
        FinalScore.text = "Score: " + GameState.GetScore();

        float elapsedTime = GameController.GetInstance().GetElapsedTime();
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        FinalTime.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        if(reason == 0)
        {
            GameOverReason.text = "Block reached border";
        }else if(reason == 1)
        {
            GameOverReason.text = "Ran out of lives";
        }
    }

    public void SaveScore()
    {
        if (string.IsNullOrWhiteSpace(NameField.text))
            return;

        ScoresManager.AddScore(new ScoreEntry(NameField.text, GameState.GetScore()));
        ScoresManager.SaveScores();
        Return();
    }

    public void Return()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
