using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool paused = false;

    public GameObject pauseMenu;

    public GameObject settingsMenu;
    public GameObject pauseMenuButtons;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            
        }
	}

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        paused = true;
        pauseMenu.SetActive(true);
        SettingsMenuHide();
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        paused = false;
        pauseMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SettingsMenuShow()
    {
        settingsMenu.SetActive(true);
        pauseMenuButtons.SetActive(false);
    }
    public void SettingsMenuHide()
    {
        settingsMenu.SetActive(false);
        pauseMenuButtons.SetActive(true);
    }
}
