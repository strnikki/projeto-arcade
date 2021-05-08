using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    private void Start() 
    {
        AudioManager.instance.Play("Main Menu Theme");
    }
    
    public void StartGame()
    {
        AudioManager.instance.Stop("Main Menu Theme");
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
