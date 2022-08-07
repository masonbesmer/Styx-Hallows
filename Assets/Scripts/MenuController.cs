using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject EscMenu;
    public GameObject SettingsMenu;
    public GameObject PlayerHud;
    public GameObject Player;

    public bool EscapeMenuOpen;
    public bool MainMenuOpen;
    public bool SettingsMenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Time.timeScale = 0;
        EscapeMenuOpen = false;
        SettingsMenuOpen = false;
        MainMenuOpen = true;
        (Player.GetComponent("ThirdPersonMovement") as MonoBehaviour).enabled = false;
        Cursor.lockState = CursorLockMode.None;
        MainMenu.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MainMenuOpen != true && SettingsMenuOpen != true)
        {
            if (EscapeMenuOpen == false)
            {
                Debug.Log("Escape");
                Time.timeScale = 0;
                EscapeMenuOpen = true;
                (Player.GetComponent("ThirdPersonMovement") as MonoBehaviour).enabled = false;
                Cursor.lockState = CursorLockMode.None;
                EscMenu.SetActive(true);
            }
            else
            {
                Resume();
            }
        }
    }

    public void Play()
    {
        Debug.Log("Play");
        MainMenu.SetActive(false);
        MainMenuOpen = false;
        Time.timeScale = 1;
        (Player.GetComponent("ThirdPersonMovement") as MonoBehaviour).enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        Application.Quit(); //For actual Application
        UnityEditor.EditorApplication.isPlaying = false; //For Editor
    }

    public void Resume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
        EscapeMenuOpen = false;
        (Player.GetComponent("ThirdPersonMovement") as MonoBehaviour).enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        EscMenu.SetActive(false);
    }

    public void Settings()
    {
        Debug.Log("Settings");
        if (EscapeMenuOpen == true)
        {
            EscMenu.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(false);
        }

        SettingsMenu.SetActive(true);
        SettingsMenuOpen = true;
    }

    public void SaveAndExit()
    {
        Debug.Log("Save and Exit");
        SettingsMenu.SetActive(false);
        SettingsMenuOpen = false;

        if (EscapeMenuOpen == true)
        {
            EscMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }
}
