using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Slider healthBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public GameObject gameOverPanel;

    private int score = 0;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public float timeBetweenWaves = 5f;

    private int zombiesAliveCount = 0;

    void Awake()
    {
        instance = this;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWave++;
        waveText.text = "Wave: " + currentWave;

        int zombieCount = GetZombieCountForWave(currentWave);
        zombiesAliveCount = zombieCount;

        ZombieSpawner.instance.SpawnWave(zombieCount);
    }

    int GetZombieCountForWave(int wave)
    {
        // Her wave'de 3 zombi daha fazla gelsin: Wave 1 = 5, Wave 2 = 8, Wave 3 = 11...
        return 5 + (wave - 1) * 3;
    }

    public void OnZombieKilled()
    {
        zombiesAliveCount--;

        if (zombiesAliveCount <= 0)
        {
            Invoke(nameof(StartNextWave), timeBetweenWaves);
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}