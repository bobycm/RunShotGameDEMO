using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isGameOver = false;
    public int score = 0;
    public float survivalTime { get; private set; } = 0f;

    public ObjectSpawnSystem spawnSystem;
    public UIControl uiControl;

    private int lastHealthBonusLevel = 0;
    private int lastSpawnBonusLevel = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
            
            if (uiControl != null)
            {
                uiControl.UpdateTime(survivalTime);
            }

            CheckDifficultyScaling();
        }
    }

    private void CheckDifficultyScaling()
    {
        int currentHealthLevel = (int)(survivalTime / 30f);
        int currentSpawnLevel = (int)(survivalTime / 60f);

        bool healthUp = currentHealthLevel > lastHealthBonusLevel;
        bool spawnUp = currentSpawnLevel > lastSpawnBonusLevel;

        if (healthUp || spawnUp)
        {
            lastHealthBonusLevel = currentHealthLevel;
            lastSpawnBonusLevel = currentSpawnLevel;

            string msg = "";
            if (healthUp && spawnUp) msg = "EnemyHP UP!\nEnemy SpawnRate UP!";
            else if (healthUp) msg = "EnemyHP UP!";
            else if (spawnUp) msg = "Enemy SpawnRate UP!";

            if (uiControl != null) uiControl.ShowAlert("WARNING\n" + msg);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (spawnSystem != null) spawnSystem.enabled = false;

        if (uiControl != null)
        {
            uiControl.ShowGameOverPanel();
            uiControl.HidePausePanel();
        }

    }
}
