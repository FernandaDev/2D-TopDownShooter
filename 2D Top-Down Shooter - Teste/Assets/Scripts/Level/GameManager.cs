using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public string PlayerName => gameInfo.playerName;
    public int Score
    {
        get => gameInfo.score;
        private set
        {
            if (gameInfo.score != value)
            {
                gameInfo.score = value;
                OnScoreChanged?.Invoke(value);
            }
        }
    }
    public float GameTime
    {
        get => gameInfo.gameTime;
        private set
        {
            if (gameInfo.gameTime != value)
            {
                gameInfo.gameTime = value;
                OnTimeChanged?.Invoke(value);
            }
        }
    }
    public event Action<int> OnScoreChanged;
    public event Action<float> OnTimeChanged;

    [SerializeField] PlayerInfo gameInfo;
    LevelLoader levelLoader;
    int enemiesCount;

    protected override void Awake()
    {
        base.Awake();
        levelLoader = FindObjectOfType<LevelLoader>();
        enemiesCount = FindObjectsOfType<AIController>().Length;
    }

    private void Start()
    {
        ResetGameInfo();
    }

    private void OnEnable() => CharacterHealth.OnDeath += OnCharacterKilled;

    private void OnDisable() => CharacterHealth.OnDeath -= OnCharacterKilled;

    private void Update()
    {
        GameTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            levelLoader.StartGame();
    }

    void OnCharacterKilled(CharacterHealth deadCharacter)
    {
        AIController AICharacter = deadCharacter.GetComponent<AIController>();

        if (AICharacter != null)
        {
            AddScore(AICharacter.ScorePointsToGive);
            AddEmenyKilledCount();
            if (gameInfo.enemiesKilled >= enemiesCount)
                levelLoader.WinGame();
        }
        else // O jogador morreu
            levelLoader.GameOver();
    }

    public void AddScore(int amount) => Score += amount;
    public void AddEmenyKilledCount() => gameInfo.enemiesKilled++;

    void ResetGameInfo()
    {
        Score = 0;
        GameTime = 0;
        gameInfo.enemiesKilled = 0;
    }
}
