using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;

    public bool MainMenuOpen;
    public bool SettingsMenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        SettingsMenuOpen = false;
        MainMenuOpen = true;
        Cursor.lockState = CursorLockMode.None;
        MainMenu.SetActive(true);
    }

    public void Play()
    {
        Debug.Log("Play");
        MainMenu.SetActive(false);
        MainMenuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit(); //For actual Application
        UnityEditor.EditorApplication.isPlaying = false; //For Editor
    }

    public void Settings()
    {
        Debug.Log("Settings");
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        SettingsMenuOpen = true;
    }

    public void SaveAndExit()
    {
        Debug.Log("Save and Exit");
        SettingsMenu.SetActive(false);
        SettingsMenuOpen = false;
        MainMenu.SetActive(true);
    }
}
