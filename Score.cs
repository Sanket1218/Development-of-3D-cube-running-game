using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject levelUpPanel;

    [Header("Level Settings")]
    public int currentLevel = 1;

    // Level 2 unlocks at 20, Level 3 at 40, Level 4 at 60, Level 5 at 80
    private double[] levelThresholds = { 20, 40, 60, 80 };

    [Header("Speed Boost Per Level Up")]
    public PlayerScript playerScript;
    public float forceIncreaseAmount = 300f;  // boosts forward movement (Rigidbody force)
    public float sideSpeedIncrease = 2f;      // boosts left/right movement

    private double myScore = 0;
    private bool levelingUp = false;

    void Update()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt((float)myScore).ToString();
        finalScoreText.text = "Score: " + Mathf.FloorToInt((float)myScore).ToString();
        CheckLevelUp();
    }

    public void AddScore(double score)
    {
        myScore = myScore + score;
    }

    void CheckLevelUp()
    {
        if (levelingUp) return;

        // thresholds: index 0 = score 20 (level 1->2), index 1 = 40 (2->3), etc.
        int thresholdIndex = currentLevel - 1;
        if (thresholdIndex >= levelThresholds.Length) return; // already at max level

        if (myScore >= levelThresholds[thresholdIndex])
        {
            currentLevel++;
            StartCoroutine(LevelUpSequence());
        }
    }

    IEnumerator LevelUpSequence()
    {
        levelingUp = true;

        // Increase BOTH force (forward speed) and speed (side movement)
        if (playerScript != null)
        {
            playerScript.force += forceIncreaseAmount;
            playerScript.speed += sideSpeedIncrease;
        }
        GameTimer gameTimer = FindObjectOfType<GameTimer>();
        if (gameTimer != null)
        gameTimer.AddBonusTime();   

        if (levelUpPanel != null)
            levelUpPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);

        levelingUp = false;
    }

    public double GetScore()
    {
        return myScore;
    }
}
