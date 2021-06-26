using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Toggle f_Toggle;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Text volumeText;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsScreen;

    private bool isFullscreen = true;

    private void Start() 
    {
        AudioManager.instance.Stop("Stage One Theme");
        AudioManager.instance.Play("Main Menu Theme");
        Cursor.lockState = CursorLockMode.None;
        f_Toggle.onValueChanged.AddListener(delegate {
            ToogleFullscreen(f_Toggle);
        });
    }
    
    public void StartGame()
    {
        AudioManager.instance.Stop("Main Menu Theme");
        SceneManager.LoadScene("MainScene");
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CreditsScreen()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void ChangeResolution(int val)
    {
        switch(val)
        {
            case 0:
                Screen.SetResolution(2560, 1080, isFullscreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, isFullscreen);
                break;
            case 2:
                Screen.SetResolution(1440, 900, isFullscreen);
                break;
            case 3:
                Screen.SetResolution(1024, 768, isFullscreen);
                break;
            case 4:
                Screen.SetResolution(800, 600, isFullscreen);
                break;
            case 5:
                Screen.SetResolution(640, 480, isFullscreen);
                break;
        }
    }

    public void ToogleFullscreen(Toggle change)
    {
        isFullscreen = f_Toggle.isOn;
        Screen.fullScreen = isFullscreen;
        Debug.Log(f_Toggle.isOn);
    }

    public void UpdateVolume()
    {
        AudioManager.instance.ChangeVolume(volumeSlider.value/100);
        volumeText.text = "" + volumeSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
