using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject quitOption;
    public GameObject pauseEventSystem;
    public Button resumeButton;
    public Button quitButton;

    public bool paused;

    private void Start()
    {
        paused = false;
    }


    private void Update()
    {
        Pause();
    }

    public void Pause()
    {
        //if ((mmenu_Active == false && testing == true) || testing == false)
        
            // Debug.Log("Active?");
            if (Input.GetButton("Pause"))
            {
                if (!paused)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    if (pauseMenu != null)
                    {
                        pauseMenu.SetActive(true);

                    }
                    Time.timeScale = 0;
                    //paused = true;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    if (pauseMenu != null)
                    {
                        pauseMenu.SetActive(false);
                        quitOption.SetActive(false);
                        resumeButton.interactable = true;
                        quitButton.interactable = true;
                    }
                    Time.timeScale = 1;
                    //paused = false;
                }
            }

            if (Input.GetButtonUp("Pause"))
            {
                if (!paused)
                    paused = true;
                else if (paused)
                {
                    //Cursor.visible = false;
                    //Cursor.lockState = CursorLockMode.Locked;
                    paused = false;
                }
            }
        
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void QuitGame()
    {
        //pauseMenu.SetActive(false);
        resumeButton.interactable = false;
        quitButton.interactable = false;
        quitOption.SetActive(true);
        pauseEventSystem.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }

    public void CancelQuit()
    {
        quitOption.SetActive(false);
        resumeButton.interactable = true;
        quitButton.interactable = true;
        pauseEventSystem.SetActive(true);
    }

    public void QuitApplication()
    {
       
        Debug.Log("Quit");
        Application.Quit();
        
    }


}
