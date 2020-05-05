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

    public int tempSceneIndex;

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
    public bool underStumpLightsOn;
    [Space(5)]
    public bool starBranchDown;
    public bool willowBranchDown;

    [Header("Tutorial Area")]
    public bool area_Tutorial = false;
    public bool tutorial_Interacted_BreakableBark = false;
    public bool tutorial_Interacted_Cordyceps = false;

    [Space(5)]
    [Header("Stinkhorn")]
    public bool area_Stinkhorn = false;
    public bool stinkhorn_bus_Called = false;
    [Space(5)]
    public bool stinkhorn_HasTalked_Rootford_Intro1 = false;
    public bool stinkhorn_HasTalked_Rootford_Intro2 = false;
    [Space(5)]
    public bool stinkhorn_HasTalked_BusDriver_1 = false;

     

    [Space(5)]
    [Header("TreeSeat")]
    public bool area_TreeSeat = false;

    //[Space(5)]
    //public bool treeSeat_HasTalked_Mulchant_GaveBottle = false;
    //public bool
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
    //public RenderTexture angelTexture;
    //public RenderTexture starTexture;
    //public RenderTexture willowTexture;

    public Texture angelTexture;
    public Texture starTexture;
    public Texture willowTexture;
    //public TextMeshProUGUI mulchCount;
    //public Image[] healthCounter;

    public Camera mainCamera;
    public delegate void UpdateCameras(Camera newCamera);
    public UpdateCameras updateCameras;

    public delegate void OnLevelLoaded();
    public OnLevelLoaded onLevelLoaded;

    public int sceneIndex;

    public Vector3 branchEndPos;

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

        //playerController.acorns = playerAcorns;

        if (acornCount != null)
            acornCount.text = playerAcorns.ToString();

        onLevelLoaded += SaveGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isDead = false;
        if (playerController != null)
        {
            Invoke("SetAcorns", 1);
        }


        if (mainMenu != null)
        {
            mainMenu.sensitivityValue.text = lookSensitivityX.ToString();
        }

        else if (pauseMenu != null)
        {
            pauseMenu.sensitivityValue.text = lookSensitivityX.ToString();
        }
        //oldMulch = playerMulch;
        tempSceneIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        #region MenuTest
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
        #endregion
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
        //TODO: needs to be modified to stop resetting acorns
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
        stinkhorn_bus_Called = data.tutorial_bus_Called;
        stinkhorn_HasTalked_Rootford_Intro1 = data.tutorial_HasTalked_Rootford_Intro1;
        stinkhorn_HasTalked_Rootford_Intro2 = data.tutorial_HasTalked_Rootford_Intro2;
        stinkhorn_HasTalked_BusDriver_1 = data.tutorial_HasTalked_BusDriver_1;
    }

    public void UpdateLevel()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        onLevelLoaded();
    }
}
