using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController Instance;

    [SerializeField] List<GameObject> BlocksToSpawn = new List<GameObject>();

    [SerializeField] List<BlockController> Blocks = new List<BlockController>();

    [SerializeField] Transform LevelArea;

    [SerializeField] List<Ball> Balls = new List<Ball>();

    [SerializeField] PaddleController Paddle;

    [SerializeField] GUIController GUI;
    [SerializeField] GameOverController GameOver;
    [SerializeField] GameObject GameOverPanel;

    [SerializeField] float elapsedTime = 0f;

    IEnumerator GameLoopCoroutine;
    bool GameLost = false;

    public static GameController GetInstance()
    {
        return Instance;
    }

    public bool GetGameLost()
    {
        return GameLost;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void DestroyedBlock(BlockController blockController)
    {
        if (GameLost)
            return;

        Blocks.Remove(blockController);

        GameState.AddScore(blockController.GetScore());

        GUI.UpdateScore(GameState.GetScore());
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Blocks = LevelArea.GetComponentsInChildren<BlockController>().ToList();

        SpawnBallForPaddle();

        GameLoopCoroutine = GameLoop();
        StartCoroutine(GameLoopCoroutine);
    }

    IEnumerator GameLoop()
    {
        while(true)
        {
            SpawnBlockGroup();

            switch (GameState.GetDifficulty())
            {
                default:
                case Difficulty.Easy:
                    yield return new WaitForSeconds(30f);
                    break;
                case Difficulty.Normal:
                    yield return new WaitForSeconds(20f);
                    break;
                case Difficulty.Hard:
                    yield return new WaitForSeconds(10f);
                    break;
            }
        }
    }

    void SpawnBlockGroup()
    {
        float startX = -15f;

        for (int i = 0; i < 12; i++)
        {
            var blockPrefab = GetRandomBlockPrefab();

            BlockController bc = Instantiate(blockPrefab, new Vector3(startX + (2.75f * i), 16), Quaternion.identity, LevelArea).GetComponent<BlockController>();

            Blocks.Add(bc);

            bc.Initialize(UnityEngine.Random.Range(1, 3));
        }

        foreach (var block in Blocks)
        {
            block.StartMove();
        }

    }

    GameObject GetRandomBlockPrefab()
    {
        float baseWeight = 80f;
        float specialBlockWeight = GetSpecialBlockWeight();

        List<float> weights = new List<float> { baseWeight };
        for (int i = 1; i < BlocksToSpawn.Count; i++)
        {
            weights.Add(specialBlockWeight);
        }

        float totalWeight = weights.Sum();
        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float currentWeightSum = 0;

        for (int i = 0; i < weights.Count; i++)
        {
            currentWeightSum += weights[i];
            if (randomValue <= currentWeightSum)
            {
                return BlocksToSpawn[i];
            }
        }

        return BlocksToSpawn[0];
    }

    float GetSpecialBlockWeight()
    {
        // Adjust weights based on difficulty
        switch (GameState.GetDifficulty())
        {
            default:
            case Difficulty.Easy:
                return 20f;
            case Difficulty.Normal:
                return 10f;
            case Difficulty.Hard:
                return 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Game speed control
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 3f;
        }

        if (!GameLost)
        {
            elapsedTime += Time.deltaTime;
            GUI.UpdateTimer(elapsedTime);
        }
        GUI.UpdateLives(GameState.GetLives());
    }

    public void SpawnBallForPaddle()
    {
        Paddle.AddBall();
    }

    public void BlockReachedBorder()
    {
        if (GameLost)
            return;

        StopCoroutine(GameLoopCoroutine);
        GameLost = true;
        GameOver.Setup(0);
        GameOverPanel.SetActive(true);
    }

    public void BallDestroyed(Ball ballController)
    {
        if (GameLost)
            return;

        Balls.Remove(ballController);

        if (Balls.Count == 0)
        {
            GameState.SetLives(GameState.GetLives() - 1);
        }

        if (GameState.GetLives() > 0)
        {
            SpawnBallForPaddle();
        }
        else
        {
            StopCoroutine(GameLoopCoroutine);
            GameLost = true;
            GameOver.Setup(1);
            GameOverPanel.SetActive(true);
        }
    }
}
