using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public CutsceneManager cutsceneManager;

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject quitOption;
    public Button resumeButton;
    public Button quitButton;
    public PlayerController playerController;
    //public Transform playerRespawn;
    public bool levelStart;
    //public bool areaSpawnCalc;

    public int playerAcorns;
    private int oldAcorns;
    public int playerMulch;
    private int oldMulch;

    public int playerHealth;
    private int oldHealth;

    public TextMeshProUGUI acornCount;
    public TextMeshProUGUI mulchCount;
    public Image[] healthCounter;

    public bool isDead;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            playerController = player.GetComponent<PlayerController>();
            if(playerController == null)
            {
                Debug.LogWarning("Player missing playercontroller!");
            }
        }
        else
        {
            Debug.LogWarning("Can't find player!");
        }

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        
        acornCount.text = playerAcorns.ToString();
        mulchCount.text = playerMulch.ToString();

        //playerController.currentSpawn = playerRespawn;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       isDead = false;
       paused = false;
       oldHealth = playerHealth;
       oldAcorns = playerAcorns;
       oldMulch = playerMulch;
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
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
        if(playerController != null)
        {
            Pause();
            PickUpCount();
            HealthCount();
        }
        //if(isDead)
        //{
        //    Reset();
        //}
    }

    public void PickUpCount()
    {
        playerAcorns = playerController.acorns;
        playerMulch = playerController.mulch;

        if (oldAcorns != playerAcorns)
        {
            acornCount.text = playerAcorns.ToString();
            oldAcorns = playerAcorns;
        }
        if(oldMulch != playerMulch)
        {
            mulchCount.text = playerMulch.ToString();
            oldMulch = playerMulch;
        }
    }

    public void HealthCount()
    {
        playerHealth = playerController.health;
        if (oldHealth != playerHealth)
        {
            for(int i = 0; i < healthCounter.Length; i++)
            {
                if(i + 1 > playerHealth)
                {
                    healthCounter[i].enabled = false;
                }
                else if(i + 1 <= playerHealth)
                {
                    healthCounter[i].enabled = true;
                }
            }
            if(playerHealth < 1)
            {
                isDead = true;
            }
            oldHealth = playerHealth;
        }
    }

    public void Reset()
    {
        playerHealth = 3;
        isDead = false;
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
