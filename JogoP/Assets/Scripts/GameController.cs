using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text heathText;
    public int score;
    public Text scoreText;
    public GameObject pauseObj;
    public GameObject gameOverObj;
    public int totalScore;
    public static GameController instance;
    private bool isPaused;
    private bool isGameOver;

    void Awake()
    {
        instance = this;
        isGameOver = false;
    }

    void Start()
    {
        totalScore = PlayerPrefs.GetInt("Score");
    }

    void Update()
    {
        PauseGame();
    }
    
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("Score", score + totalScore);
    }
    public void UpdateLives(int value)
    {
        heathText.text = "x " + value.ToString();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
        if (isGameOver == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            pauseObj.SetActive(isPaused);

            if (isPaused == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}
