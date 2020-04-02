using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject quitOption;
    public GameObject pauseEventSystem;
    public Button resumeButton;
    public Button quitButton;

    public RawImage acornDisplay;
    public TextMeshProUGUI acornText;

    public RawImage brownBottle;
    public RawImage greenBottle;
    public RawImage yellowBottle;

    GameController gameController;

    public bool paused;

    private void Start()
    {
        paused = false;

        gameController = GameController.Instance;

        //acornDisplay = GetComponent<RawImage>();
        //acornText = GetComponent<TextMeshProUGUI>();

        //brownBottle = GetComponent<RawImage>();
        //greenBottle = GetComponent<RawImage>();
        //yellowBottle = GetComponent<RawImage>();
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
                        acornDisplay.enabled = true;
                        acornText.enabled = true;
                        if(gameController.hasBottles)
                        {
                            brownBottle.enabled = true;
                            greenBottle.enabled = true;
                            yellowBottle.enabled = true;
                        }
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
                acornDisplay.enabled = false;
                acornText.enabled = false;
                if (gameController.hasBottles)
                {
                    brownBottle.enabled = false;
                    greenBottle.enabled = false;
                    yellowBottle.enabled = false;
                }
                paused = false;
                }
            }
        
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        acornDisplay.enabled = false;
        acornText.enabled = false;
        if (gameController.hasBottles)
        {
            brownBottle.enabled = false;
            greenBottle.enabled = false;
            yellowBottle.enabled = false;
        }
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
