using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject quitOption;
    public Button resumeButton;
    public Button quitButton;

    //-----------------------------------------------------------------

    public bool testing;

    //-----------------------------------------------------------------
    //Only for testing both Main and Pause Menu in same scene;
    //Turn this off on MainMenuHolder AND GameController gameobjects
    //once MainMenu has its own scene

    public bool paused;
    public bool mmenu_Active;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(mainMenu.activeInHierarchy == true)
        {
            mmenu_Active = true;
        }
        else
        {
            mmenu_Active = false;
        }

        Pause();
    }

    public void Pause()
    {
        if(mmenu_Active == false)
        {
            Debug.Log("Active?");
            if (Input.GetButton("Pause"))
            {
                if (!paused)
                {
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                }
                else
                {
                    Cursor.visible = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //pauseMenu.SetActive(false);
        resumeButton.interactable = false;
        quitButton.interactable = false;
        quitOption.SetActive(true);
    }

    public void QuitToMenu()
    {
        if(testing)
        {
            pauseMenu.SetActive(false);
            quitOption.SetActive(false);
            mainMenu.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void CancelQuit()
    {
        quitOption.SetActive(false);
        resumeButton.interactable = true;
        quitButton.interactable = true;
    }

    public void QuitApplication()
    {
        if(testing)
        {
            Debug.Log("Quit");
        }
        else
        {
            Application.Quit();
        }
    }
}
