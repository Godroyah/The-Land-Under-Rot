using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Script Calls")]
    #region Script Calls
    private static GameController _instance = null;
    public static GameController Instance { get { return _instance; } }
    public DialogueManager dialogueManager;
    public CutsceneManager cutsceneManager;
    public PlayerController playerController;
    public CamControl camControl;
    public PauseMenu pauseMenu;
    public MainMenu mainMenu;
    #endregion

    //[Header("Menu Components")]
    //#region Menu Components
    //public GameObject pauseMenu;
    //public GameObject quitOption;
    //public GameObject pauseEventSystem;
    //public Button resumeButton;
    //public Button quitButton;
    //#endregion

    [Header("Bools")]
    #region Bools
    public bool levelStart;
    public bool isDead;
    //public bool paused;

    [Space(5)]
    public bool mulchant_GivenBottles;
    [Space(5)]
    public bool hasBrownMulch;
    public bool hasGreenMulch;
    public bool hasYellowMulch;
    [Space(5)]
    public bool angelTreeAwake;
    public bool starTreeAwake;
    public bool willowTreeAwake;
    [Space(5)]
    public bool revealNewAreas;
    //TODO: need a better way to set worms on and off
    public bool wormsInFruitfulGone;

    [Space(5)]
    public bool area_Tutorial = false;
    public bool tutorial_bus_Called = false;
    [Space(5)]
    public bool tutorial_HasTalked_Rootford_Intro1 = false;
    public bool tutorial_HasTalked_Rootford_Intro2 = false;
    [Space(5)]
    public bool tutorial_HasTalked_BusDriver_1 = false;

    //[Space(5)]
    //public bool treeSeat_HasTalked_Mulchant_GaveBottle = false;
    //public bool 

    [Space(5)]

    public bool area_Stinkhorn = false;
    public bool area_TreeSeat = false;
    #endregion

    [Header("Look Sensitivity")]
    //public GameObject sensitivitySlider;
    public Slider sensitivityBar;
    public float lookSensitivityX = 5.0f;
    public float lookSensitivityY = 5.0f;
    //10 initially for both

    [Header("Inventory Count")]
    #region Inventory Count
    public int playerAcorns;
    private int oldAcorns;
    //public int playerMulch;
    //private int oldMulch;
    [Space(2)]
    //public int playerHealth;
    //private int oldHealth;
    #endregion

    [Header("Player UI Components")]

    public TextMeshProUGUI acornCount;
    [Space(5)]
    public RawImage brownBottle;
    public RawImage greenBottle;
    public RawImage yellowBottle;
    [Space(5)]
    public RenderTexture angelTexture;
    public RenderTexture starTexture;
    public RenderTexture willowTexture;
    //public TextMeshProUGUI mulchCount;
    //public Image[] healthCounter;

    public Camera mainCamera;
    public delegate void UpdateCameras(Camera newCamera);
    public UpdateCameras updateCameras;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        //if (playerController == null)
        //{
        //    playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //    if (playerController == null)
        //    {
        //        Debug.LogWarning("Cannot find Player!");
        //    }
        //    else
        //        playerController.gameController = this;
        //}


        //if (player)
        //{
        //    playerController = player.GetComponent<PlayerController>();
        //    if (playerController == null)
        //    {
        //        Debug.LogWarning("Player missing playercontroller!");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("Can't find player!");
        //}

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (acornCount != null)
            acornCount.text = playerAcorns.ToString();
        

        //if (mulchCount != null)
        //    mulchCount.text = playerMulch.ToString();

        //playerController.currentSpawn = playerRespawn;

        //GameObject temp = GameObject.Find("@GameController");
        //if (!temp)
        //    DontDestroyOnLoad(gameObject);
        //else
        //    Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        //oldHealth = playerHealth;
        if(playerController != null)
        {
            Invoke("SetAcorns", 1);
        }

        if(mainMenu != null)
        {
            mainMenu.sensitivityValue.text = lookSensitivityX.ToString();
        }

        else if (pauseMenu != null)
        {
            pauseMenu.sensitivityValue.text = lookSensitivityX.ToString();
        }
        //oldMulch = playerMulch;
    }

    // Update is called once per frame
    void Update()
    {
        //if (testing)
        //{
        //    if (mainMenu.activeInHierarchy == true)
        //    {
        //        mmenu_Active = true;
        //    }
        //    else
        //    {
        //        mmenu_Active = false;
        //    }
        //}
        if (playerController != null)
        {
            
            PickUpCount();
            //HealthCount();
        }

        if (playerController != null && dialogueManager != null)
        {
            if (dialogueManager.hasActiveDialogue)
            {
                playerController.StopPlayer = true;
                playerController.camControl.lockPosition = true;
            }
            else
            {
                playerController.StopPlayer = false;
                playerController.camControl.lockPosition = false;
            }
                
        }
        //if(isDead)
        //{
        //    Reset();
        //}

    }

    public void SensitivityValueCheckPause()
    {
        lookSensitivityX = sensitivityBar.value;
        lookSensitivityY = sensitivityBar.value;
        camControl.sensitivity_X = lookSensitivityX;
        camControl.sensitivity_Y = lookSensitivityY;
        pauseMenu.sensitivityValue.text = pauseMenu.sensitivitySlider.value.ToString();
    }

    public void SensitivityValueCheckMain()
    {
        lookSensitivityX = sensitivityBar.value;
        lookSensitivityY = sensitivityBar.value;
        //camControl.sensitivity_X = lookSensitivityX;
        //camControl.sensitivity_Y = lookSensitivityY;
        mainMenu.sensitivityValue.text = mainMenu.sensitivitySlider.value.ToString();
    }

    public void SetAcorns()
    {
        playerAcorns = playerController.acorns;
        acornCount.text = playerAcorns.ToString();
        oldAcorns = playerAcorns;
    }


    public void FillBrownBottle()
    {
        brownBottle.texture = angelTexture;
    }

    public void FillGreenBottle()
    {
        greenBottle.texture = starTexture;
    }

    public void FillYellowBottle()
    {
        yellowBottle.texture = willowTexture;
    }

    public void PickUpCount()
    {
        playerAcorns = playerController.acorns;
        //playerMulch = playerController.mulch;

        if (oldAcorns != playerAcorns)
        {
            if (acornCount != null)
            {
                acornCount.text = playerAcorns.ToString();
                oldAcorns = playerAcorns;
            }
        }
        //if (oldMulch != playerMulch)
        //{
        //    if (mulchCount != null)
        //    {
        //        mulchCount.text = playerMulch.ToString();
        //        oldMulch = playerMulch;
        //    }
        //}
    }

    //public void HealthCount()
    //{
    //    playerHealth = playerController.health;
    //    if (oldHealth != playerHealth)
    //    {
    //        for (int i = 0; i < healthCounter.Length; i++)
    //        {
    //            if (i + 1 > playerHealth)
    //            {
    //                healthCounter[i].enabled = false;
    //            }
    //            else if (i + 1 <= playerHealth)
    //            {
    //                healthCounter[i].enabled = true;
    //            }
    //        }
    //        if (playerHealth < 1)
    //        {
    //            isDead = true;
    //        }
    //        oldHealth = playerHealth;
    //    }
    //}

    //public void Reset()
    //{
    //    playerHealth = 3;
    //    isDead = false;
    //}

    public void UpdateMainCamera(Camera newCamera)
    {
        updateCameras(newCamera);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        playerAcorns = data.playerAcorns;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        mulchant_GivenBottles = data.mulchant_GivenBottles;

        hasBrownMulch = data.hasBrownMulch;
        hasGreenMulch = data.hasGreenMulch;
        hasYellowMulch = data.hasYellowMulch;

        angelTreeAwake = data.angelTreeAwake;
        starTreeAwake = data.starTreeAwake;
        willowTreeAwake = data.willowTreeAwake;

        revealNewAreas = data.revealNewAreas;

        area_Tutorial = data.area_Tutorial;
        tutorial_bus_Called = data.tutorial_bus_Called;
        tutorial_HasTalked_Rootford_Intro1 = data.tutorial_HasTalked_Rootford_Intro1;
        tutorial_HasTalked_Rootford_Intro2 = data.tutorial_HasTalked_Rootford_Intro2;
        tutorial_HasTalked_BusDriver_1 = data.tutorial_HasTalked_BusDriver_1;
    }
}
