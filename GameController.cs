using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject tapToStart;
    public GameObject scoreText;
    public GameObject levelUpPanel;  
    public GameTimer gameTimer;    // drag your LevelUpPanel here too

    public void Start()
    {
        gameOverPanel.SetActive(false);
        tapToStart.SetActive(true);
        scoreText.SetActive(false);
         gameTimer.StartTimer();

        // Make sure levelUpPanel is hidden at start
        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);

        PauseGame();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void GameOver()
    {
        scoreText.SetActive(false);

        // Hide level up panel if it happens to be showing
        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);

        gameOverPanel.SetActive(true);
            gameTimer.StopTimer();
    }

    public void Restart()
    {
        // Use the actual scene name — change "Game" if your scene is named differently
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        tapToStart.SetActive(false);
        Time.timeScale = 1f;
        scoreText.SetActive(true);
    }
}
