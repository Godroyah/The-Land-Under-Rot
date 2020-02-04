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
    public PlayerController playerController;
    //public Transform playerRespawn;
    public bool levelStart;
    public bool areaSpawnCalc;

    //-----------------------------------------------------------------

    public bool testing;

    //-----------------------------------------------------------------
    //Only for testing both Main and Pause Menu in same scene;
    //Turn this off on MainMenuHolder AND GameController gameobjects
    //once MainMenu has its own scene

    public bool paused;
    public bool mmenu_Active;

    private void Awake()
    {
        //playerController.currentSpawn = playerRespawn;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(testing)
        {
            if (mainMenu.activeInHierarchy == true)
            {
                mmenu_Active = true;
            }
            else
            {
                mmenu_Active = false;
            }
        }
        Pause();
    }

    public void Pause()
    {
        if((mmenu_Active == false && testing == true) || testing == false)
        {
           // Debug.Log("Active?");
            if (Input.GetButton("Pause"))
            {
                if (!paused)
                {
                    Cursor.visible = true;
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    Cursor.visible = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            }

            if(Input.GetButtonUp("Pause"))
            {
                if (!paused)
                    paused = true;
                else if (paused)
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
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        if (testing)
        {
            pauseMenu.SetActive(false);
            quitOption.SetActive(false);
            resumeButton.interactable = true;
            quitButton.interactable = true;
            mainMenu.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene(0);
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
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
