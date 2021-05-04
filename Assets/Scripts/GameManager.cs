using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;

    private Player player;

    private int score = 0;
    private bool isPaused = false;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";

        // Impede o cursor de sair da tela
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        player.StopScripts();
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        player.StartScripts();
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene("PrototypeScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
