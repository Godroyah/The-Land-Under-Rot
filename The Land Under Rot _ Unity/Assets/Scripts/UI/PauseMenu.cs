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
    //public GameObject pauseEventSystem;
    public GameObject decoration;
    //public Button resumeButton;
    //public Button quitButton;

    //public RawImage acornDisplay;
    public TextMeshProUGUI acornText;

    public RawImage angelBottle;
    public RawImage starBottle;
    public RawImage willowBottle;

    GameController gameController;

    public bool paused;

    private void Start()
    {
        paused = false;

        gameController = GameController.Instance;
    }

    private void Update()
    {
        Pause();
    }

    public void Pause()
    {
        //THIS RIGHT HERE IS A JANK SOLUTION. Pausing keeps deleting the 3rd bottle reference in SelfReport (which right now is the green bottle but used to be the yellow bottle
        //before I put in the NONE enum. NEED to come back and fix this at a later date but for now this is working.
        gameController.greenBottle = starBottle;
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
                        decoration.SetActive(true);
                       // acornDisplay.enabled = true;
                        acornText.enabled = true;
                        if(gameController.mulchant_GivenBottles)
                        {
                            if(!gameController.angelTreeAwake)
                            angelBottle.enabled = true;
                            if(!gameController.starTreeAwake)
                            starBottle.enabled = true;
                            if(!gameController.willowTreeAwake)
                            willowBottle.enabled = true;
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
                        decoration.SetActive(false);
                        acornText.enabled = false;
                    //resumeButton.interactable = true;
                    //quitButton.interactable = true;
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
                //acornDisplay.enabled = false;
                acornText.enabled = false;
                if (gameController.mulchant_GivenBottles)
                {
                    angelBottle.enabled = false;
                    starBottle.enabled = false;
                    willowBottle.enabled = false;
                }
                paused = false;
                }
            }
        
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        decoration.SetActive(false);
        //acornDisplay.enabled = false;
        acornText.enabled = false;
        if (gameController.mulchant_GivenBottles)
        {
            angelBottle.enabled = false;
            starBottle.enabled = false;
            willowBottle.enabled = false;
        }
        Time.timeScale = 1;
        paused = false;
    }

    public void QuitGame()
    {
        //pauseMenu.SetActive(false);
        //resumeButton.interactable = false;
        //quitButton.interactable = false;
        quitOption.SetActive(true);
        pauseMenu.SetActive(false);
        //pauseEventSystem.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }

    public void CancelQuit()
    {
        pauseMenu.SetActive(true);
        quitOption.SetActive(false);
        //resumeButton.interactable = true;
        //quitButton.interactable = true;
        //pauseEventSystem.SetActive(true);
    }

    public void QuitApplication()
    {
       
        Debug.Log("Quit");
        Application.Quit();
        
    }


}
