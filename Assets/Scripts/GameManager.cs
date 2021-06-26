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
    [SerializeField] SpawnManager[] spawnManagers;
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossSpawn;

    private Player player;

    private int score = 0;
    private int maxScore = 0;
    private bool isPaused = false;
    private bool isGameOver = false;
    private bool stageCompleteMessage = false;
    private int currentArea = 1;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";

        // Impede o cursor de sair da tela
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.Find("Player").GetComponent<Player>();

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            maxScore = 10;
        }
        else
        {
            maxScore = 2;
        }

        AudioManager.instance.Play("Stage One Theme");
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
        AudioManager.instance.Pause("Stage One Theme");
        player.StopScripts();
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        AudioManager.instance.Play("Stage One Theme");
        player.StartScripts();
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateScore(int points)
    {
        if(stageCompleteMessage)
            return;
        score += points;
        scoreText.text = "Score: " + score;

        if(score == maxScore){
            if(SceneManager.GetActiveScene().name == "MainScene"){
                StageComplete();
            }
            else 
            {
                SpawnBoss();
            }
        }
    }

    private void SpawnBoss()
    {
        Instantiate(boss, bossSpawn.position, boss.transform.rotation);
    }

    public void StageComplete()
    {
        scoreText.text = "STAGE COMPLETE";
        stageCompleteMessage = true;
        StartCoroutine(StageTransitionCooldown());
    }

    IEnumerator StageTransitionCooldown()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Scene2");
    }

    public void ShowGameOverScreen()
    {
        AudioManager.instance.Stop("Stage One Theme");
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangePlayerArea()
    {
        if(currentArea == 1)
        {
            currentArea = 2;
            spawnManagers[0].StopSpawning();
            spawnManagers[1].StopSpawning();
            spawnManagers[2].StartSpawning();
            spawnManagers[3].StartSpawning();
        }
        else 
        {
            currentArea = 1;
            spawnManagers[0].StartSpawning();
            spawnManagers[1].StartSpawning();
            spawnManagers[2].StopSpawning();
            spawnManagers[3].StopSpawning();
        }
    }
}
