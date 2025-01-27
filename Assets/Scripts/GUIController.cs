using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text TimerText;
    [SerializeField] TMP_Text LivesText;

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void UpdateTimer(float elapsedTime)
    {
        TimerText.text = FormatTime(elapsedTime);
    }

    public void UpdateLives(int lives)
    {
        LivesText.text = lives.ToString();
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
