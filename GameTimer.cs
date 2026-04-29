using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;      // drag your existing GameOver panel here

    [Header("Settings")]
    public float timePerLevel = 15f;      // seconds given per level
    public float bonusTimeOnLevelUp = 5f; // extra seconds added on each level up

    private float timeRemaining;
    private bool timerRunning = false;

    void Start()
    {
        timeRemaining = timePerLevel;
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Update UI — show whole number
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();

            // Turn text orange when under 10 seconds
            if (timeRemaining <= 10f)
                timerText.color = new Color(1f, 0.4f, 0f); // orange
            else
                timerText.color = Color.red;
        }
        else
        {
            // Time ran out — game over
            timeRemaining = 0;
            timerText.text = "0";
            timerRunning = false;
            GameOver();
        }
    }

    // Call this from GameController when game starts
    public void StartTimer()
    {
        timerRunning = true;
    }

    // Call this from GameController when game ends
    public void StopTimer()
    {
        timerRunning = false;
    }

    // Call this from Score.cs when level goes up
    public void AddBonusTime()
    {
        timeRemaining += bonusTimeOnLevelUp;
    }

    void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Stop player
        PlayerScript player = FindObjectOfType<PlayerScript>();
        if (player != null)
            player.enabled = false;
    }
}