using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject Logo;

    [SerializeField] GameObject StartPage;
    [SerializeField] GameObject PlayPage;
    [SerializeField] GameObject ScoresPage;

    [SerializeField] GameObject ScoreCardPrefab;
    [SerializeField] RectTransform Leaderboard;

    static bool FirstStart = true;

    private void Start()
    {
        if (FirstStart)
        {
            ScoresManager.Initialize();

            FirstStart = false;

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }

    public void BtnPlay()
    {
        StartPage.SetActive(false);

        PlayPage.SetActive(true);
    }

    public void BtnEasy()
    {
        GameState.NewState(Difficulty.Easy);
        SceneManager.LoadScene(1);
    }

    public void BtnNormal()
    {
        GameState.NewState(Difficulty.Normal);
        SceneManager.LoadScene(1);
    }

    public void BtnHard()
    {
        GameState.NewState(Difficulty.Hard);
        SceneManager.LoadScene(1);
    }

    public void BtnScores()
    {
        StartPage.SetActive(false);

        foreach (RectTransform child in Leaderboard.transform)
        {
            Destroy(child.gameObject);
        }

        List<ScoreEntry> scores = ScoresManager.GetScores();
        foreach (var item in scores)
        {
            Instantiate(ScoreCardPrefab, Leaderboard).GetComponent<ScoreCard>().Initialize(item);
        }

        for (int i = scores.Count; i < 7; i++)
        {
            Instantiate(ScoreCardPrefab, Leaderboard);
        }

        ScoresPage.SetActive(true);

        Logo.SetActive(false);
    }

    public void ClearScores()
    {
        ScoresManager.ClearScores();
        ScoresManager.SaveScores();

        foreach (RectTransform child in Leaderboard.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 7; i++)
        {
            Instantiate(ScoreCardPrefab, Leaderboard);
        }
    }

    public void BtnStart()
    {
        PlayPage.SetActive(false);
        ScoresPage.SetActive(false);

        StartPage.SetActive(true);
        Logo.SetActive(true);
    }

    public void BtnExit()
    {
        Application.Quit();
    }
}
