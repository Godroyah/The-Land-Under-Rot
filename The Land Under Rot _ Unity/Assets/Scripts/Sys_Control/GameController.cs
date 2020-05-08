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
    public InputReader inputReader;
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
    public int acorns;
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
    public bool tutorial_Interacted_BlueCordyceps = false;

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

    public bool stinkhorn_Interacted_BreakableBark1 = false; //number is based on encounter order
    public bool stinkhorn_Interacted_BreakableBark2 = false;
    public bool stinkhorn_Interacted_BreakableBark3 = false;
    public bool fruitful_Interacted_BreakableBark1 = false;
    public bool fruitful_Interacted_BreakableBark2 = false;
    public bool fruitful_Interacted_BreakableBark3 = false;
    public bool fruitful_Interacted_BreakableBark4 = false;
    public bool fruitful_Interacted_BreakableBark5 = false;
    public bool stinkhorn_Interacted_BlueCordyceps1 = false;
    public bool stinkhorn_Interacted_BlueCordyceps2 = false;
    public bool stinkhorn_Interacted_Acorn1 = false;
    public bool stinkhorn_Interacted_Acorn2 = false;
    public bool stinkhorn_Interacted_Acorn3 = false;
    public bool stinkhorn_Interacted_Acorn4 = false;
    public bool stinkhorn_Interacted_Acorn6 = false;
    public bool stinkhorn_Interacted_Acorn7 = false;
    public bool stinkhorn_Interacted_Acorn8 = false;
    public bool stinkhorn_Interacted_Acorn9 = false;

    [Space(5)]
    [Header("TreeSeat")]
    public bool area_TreeSeat = false;

    [Space(5)]
    public bool treeSeat_Interacted_Acorn0 = false;
    public bool treeSeat_Interacted_Acorn1 = false;
    public bool treeSeat_Interacted_Acorn2 = false;
    public bool treeSeat_Interacted_Acorn3 = false;
    //[Space(5)]
    //public bool treeSeat_HasTalked_Mulchant_GaveBottle = false;
    //public bool

    [Space(5)]
    [Header("Fruitful")]
    public bool area_Fruitful = false;

    [Space(5)]
    public bool fruitful_Interacted_Acorn0 = false;
    public bool fruitful_Interacted_Acorn1 = false;
    public bool fruitful_Interacted_Acorn2 = false;
    public bool fruitful_Interacted_Acorn3 = false;
    #endregion

    #region InputStrings

    //Base Strings
    public string horizontalInput;
    public string verticalInput;
    public string camXInput;
    public string camYInput;
    public string headbuttInput;
    public string jumpInput;
    public string pauseInput;
    public string interactInput;
    public string submitInput;
    public string cancelInput;

    //PC Axes
    private string horizPC = "Horizontal PC";
    private string vertPC = "Vertical PC";
    private string camXPC = "CamX PC";
    private string camYPC = "CamY PC";
    private string headbuttPC = "Headbutt PC";

    //PC Buttons
    private string jumpPC = "Jump PC";
    private string pausePC = "Pause PC";
    private string interactPC = "Interact PC";
    private string submitPC = "Submit PC";
    private string cancelPC = "Cancel PC";

    //Xbox Axes
    private string horizXbox = "Horizontal Xbox";
    private string vertXbox = "Vertical Xbox";
    private string camXXbox = "CamX Xbox";
    private string camYXbox = "CamY Xbox";
    private string headbuttXbox = "Headbutt Xbox";

    //Xbox Buttons
    private string jumpXbox = "Jump Xbox";
    private string pauseXbox = "Pause Xbox";
    private string interactXbox = "Interact Xbox";
    private string submitXbox = "Submit Xbox";
    private string cancelXbox = "Cancel Xbox";

    //PS4 Axes
    private string horizPS4 = "Horizontal PS4";
    private string vertPS4 = "Vertical PS4";
    private string camXPS4 = "CamX PS4";
    private string camYPS4 = "CamY PS4";
    private string headbuttPS4 = "Headbutt PS4";

    //PS4 Buttons
    private string jumpPS4 = "Jump PS4";
    private string pausePS4 = "Pause PS4";
    private string interactPS4 = "Interact PS4";
    private string submitPS4 = "Submit PS4";
    private string cancelPS4 = "Cancel PS4";
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
    public int maxAcorns = 0;
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
            acornCount.text = acorns.ToString() + "/" + maxAcorns.ToString();

        Instance.onLevelLoaded = Instance.SaveGame; // Resets the delegate every time a new level is loaded
    }

    // Start is called before the first frame update
    void Start()
    {
        //inputReader = GetComponent<InputReader>();

        horizontalInput = horizPC;
        verticalInput = vertPC;
        camXInput = camXPC;
        camYInput = camYPC;
        headbuttInput = headbuttPC;
        jumpInput = jumpPC;
        pauseInput = pausePC;
        interactInput = interactPC;
        submitInput = submitPC;
        cancelInput = cancelPC;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isDead = false;
        //if (playerController != null)
        //{
        //    Invoke("SetAcorns", 1);
        //}


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

        #region Determine Input

        Debug.Log(interactInput);

        inputReader.GetInputState();

        if(inputReader.in_State == InputReader.gameInputState.Xbox)
        {
            horizontalInput = horizXbox;
            verticalInput = vertXbox;
            camXInput = camXXbox;
            camYInput = camYXbox;
            headbuttInput = headbuttXbox;
            jumpInput = jumpXbox;
            pauseInput = pauseXbox;
            interactInput = interactXbox;
            submitInput = submitXbox;
            cancelInput = cancelXbox;
        }
        else if(inputReader.in_State == InputReader.gameInputState.PS4)
        {
            horizontalInput = horizPS4;
            verticalInput = vertPS4;
            camXInput = camXPS4;
            camYInput = camYPS4;
            headbuttInput = headbuttPS4;
            jumpInput = jumpPS4;
            pauseInput = pausePS4;
            interactInput = interactPS4;
            submitInput = submitPS4;
            cancelInput = cancelPS4;
        }
        else
        {
            horizontalInput = horizPC;
            verticalInput = vertPC;
            camXInput = camXPC;
            camYInput = camYPC;
            headbuttInput = headbuttPC;
            jumpInput = jumpPC;
            pauseInput = pausePC;
            //if(interactInput != interactPC)
            interactInput = interactPC;
            submitInput = submitPC;
            cancelInput = cancelPC;
        }

        //switch (inputReader.GetInputState())
        //{
        //    case InputReader.gameInputState.MouseKeyboard:
        //        horizontalInput = horizPC;
        //        verticalInput = vertPC;
        //        camXInput = camXPC;
        //        camYInput = camYPC;
        //        headbuttInput = headbuttPC;
        //        jumpInput = jumpPC;
        //        pauseInput = pausePC;
        //        interactInput = interactPC;
        //        submitInput = submitPC;
        //        cancelInput = cancelPC;
        //        break;
        //    case InputReader.gameInputState.Xbox:
        //        horizontalInput = horizXbox;
        //        verticalInput = vertXbox;
        //        camXInput = camXXbox;
        //        camYInput = camYXbox;
        //        headbuttInput = headbuttXbox;
        //        jumpInput = jumpXbox;
        //        pauseInput = pauseXbox;
        //        interactInput = interactXbox;
        //        submitInput = submitXbox;
        //        cancelInput = cancelXbox;
        //        break;
        //    case InputReader.gameInputState.PS4:
        //        horizontalInput = horizPS4;
        //        verticalInput = vertPS4;
        //        camXInput = camXPS4;
        //        camYInput = camYPS4;
        //        headbuttInput = headbuttPS4;
        //        jumpInput = jumpPS4;
        //        pauseInput = pausePS4;
        //        interactInput = interactPS4;
        //        submitInput = submitPS4;
        //        cancelInput = cancelPS4;
        //        break;
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
        //GameData data = SaveSystem.LoadGame();
        //playerAcorns = data.playerAcorns;
        acornCount.text = acorns.ToString() + "/" + maxAcorns.ToString();
        oldAcorns = acorns;
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
        //GameData data = SaveSystem.LoadGame();
        //playerAcorns = playerController.acorns;
        //data.playerAcorns = playerAcorns;

        //playerMulch = playerController.mulch;

        if (oldAcorns != acorns)
        {
            if (acornCount != null)
            {
                acornCount.text = acorns.ToString() + "/" + maxAcorns.ToString();
                oldAcorns = acorns;
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

    public void InteractedWith(Interaction obj)
    {
        switch (obj)
        {
            case Interaction.NONE:
                Debug.Log("Interaction not Assigned!");
                break;
            case Interaction.Tutorial_BreakableBark:
                tutorial_Interacted_BreakableBark = true;
                break;
            case Interaction.Tutorial_BlueCordyceps:
                tutorial_Interacted_BlueCordyceps = true;
                break;
            case Interaction.Stinkhorn_BreakableBark1:
                stinkhorn_Interacted_BreakableBark1 = true;
                break;
            case Interaction.Stinkhorn_BreakableBark2:
                stinkhorn_Interacted_BreakableBark2 = true;
                break;
            case Interaction.Stinkhorn_BreakableBark3:
                stinkhorn_Interacted_BreakableBark3 = true;
                break;
            case Interaction.Stinkhorn_BlueCordyceps1:
                stinkhorn_Interacted_BlueCordyceps1 = true;
                break;
            case Interaction.Stinkhorn_BlueCordyceps2:
                stinkhorn_Interacted_BlueCordyceps2 = true;
                break;
            case Interaction.Stinkhorn_Acorn1:
                stinkhorn_Interacted_Acorn1 = true;
                break;
            case Interaction.Stinkhorn_Acorn2:
                stinkhorn_Interacted_Acorn2 = true;
                break;
            case Interaction.Stinkhorn_Acorn3:
                stinkhorn_Interacted_Acorn3 = true;
                break;
            case Interaction.Stinkhorn_Acorn4:
                stinkhorn_Interacted_Acorn4 = true;
                break;
            case Interaction.Stinkhorn_Acorn6:
                stinkhorn_Interacted_Acorn6 = true;
                break;
            case Interaction.Stinkhorn_Acorn7:
                stinkhorn_Interacted_Acorn7 = true;
                break;
            case Interaction.Stinkhorn_Acorn8:
                stinkhorn_Interacted_Acorn8 = true;
                break;
            case Interaction.Stinkhorn_Acorn9:
                stinkhorn_Interacted_Acorn9 = true;
                break;
            case Interaction.TreeSeat_Acorn0:
                treeSeat_Interacted_Acorn0 = true;
                break;
            case Interaction.TreeSeat_Acorn1:
                treeSeat_Interacted_Acorn1 = true;
                break;
            case Interaction.TreeSeat_Acorn2:
                treeSeat_Interacted_Acorn2 = true;
                break;
            case Interaction.TreeSeat_Acorn3:
                treeSeat_Interacted_Acorn3 = true;
                break;
            case Interaction.Fruitful_Acorn0:
                fruitful_Interacted_Acorn0 = true;
                break;
            case Interaction.Fruitful_Acorn1:
                fruitful_Interacted_Acorn1 = true;
                break;
            case Interaction.Fruitful_Acorn2:
                fruitful_Interacted_Acorn2 = true;
                break;
            case Interaction.Fruitful_Acorn3:
                fruitful_Interacted_Acorn3 = true;
                break;
            default:
                Debug.Log("Interaction not Assigned!");
                break;
        }
    }

    public bool HasInteracted(Interaction obj)
    {
        switch (obj)
        {
            case Interaction.NONE:
                Debug.Log("Interaction not Assigned!");
                return false;
            case Interaction.Tutorial_BreakableBark:
                return tutorial_Interacted_BreakableBark;
            case Interaction.Tutorial_BlueCordyceps:
                return tutorial_Interacted_BlueCordyceps;
            case Interaction.Stinkhorn_BreakableBark1:
                return stinkhorn_Interacted_BreakableBark1;
            case Interaction.Stinkhorn_BreakableBark2:
                return stinkhorn_Interacted_BreakableBark2;
            case Interaction.Stinkhorn_BreakableBark3:
                return stinkhorn_Interacted_BreakableBark3;
            case Interaction.Fruitful_BreakableBark1:
                return fruitful_Interacted_BreakableBark1;
            case Interaction.Fruitful_BreakableBark2:
                return fruitful_Interacted_BreakableBark2;
            case Interaction.Fruitful_BreakableBark3:
                return fruitful_Interacted_BreakableBark3;
            case Interaction.Fruitful_BreakableBark4:
                return fruitful_Interacted_BreakableBark4;
            case Interaction.Fruitful_BreakableBark5:
                return fruitful_Interacted_BreakableBark5;
            case Interaction.Stinkhorn_BlueCordyceps1:
                return stinkhorn_Interacted_BlueCordyceps1;
            case Interaction.Stinkhorn_BlueCordyceps2:
                return stinkhorn_Interacted_BlueCordyceps2;
            case Interaction.Stinkhorn_Acorn1:
                return stinkhorn_Interacted_Acorn1;
            case Interaction.Stinkhorn_Acorn2:
                return stinkhorn_Interacted_Acorn2;
            case Interaction.Stinkhorn_Acorn3:
                return stinkhorn_Interacted_Acorn3;
            case Interaction.Stinkhorn_Acorn4:
                return stinkhorn_Interacted_Acorn4;
            case Interaction.Stinkhorn_Acorn6:
                return stinkhorn_Interacted_Acorn6;
            case Interaction.Stinkhorn_Acorn7:
                return stinkhorn_Interacted_Acorn7;
            case Interaction.Stinkhorn_Acorn8:
                return stinkhorn_Interacted_Acorn8;
            case Interaction.Stinkhorn_Acorn9:
                return stinkhorn_Interacted_Acorn9;
            case Interaction.TreeSeat_Acorn0:
                return treeSeat_Interacted_Acorn0;
            case Interaction.TreeSeat_Acorn1:
                return treeSeat_Interacted_Acorn1;
            case Interaction.TreeSeat_Acorn2:
                return treeSeat_Interacted_Acorn2;
            case Interaction.TreeSeat_Acorn3:
                return treeSeat_Interacted_Acorn3;
            case Interaction.Fruitful_Acorn0:
                return fruitful_Interacted_Acorn0;
            case Interaction.Fruitful_Acorn1:
                return fruitful_Interacted_Acorn1;
            case Interaction.Fruitful_Acorn2:
                return fruitful_Interacted_Acorn2;
            case Interaction.Fruitful_Acorn3:
                return fruitful_Interacted_Acorn3 = true;
            default:
                Debug.Log("Interaction not Assigned!");
                return false;
        }
    }
}

public enum Interaction
{
    NONE,
    Tutorial_BreakableBark,
    Tutorial_BlueCordyceps,
    Stinkhorn_BreakableBark1,
    Stinkhorn_BreakableBark2,
    Stinkhorn_BreakableBark3,
    Fruitful_BreakableBark1,
    Fruitful_BreakableBark2,
    Fruitful_BreakableBark3,
    Fruitful_BreakableBark4,
    Fruitful_BreakableBark5,
    Stinkhorn_BlueCordyceps1,
    Stinkhorn_BlueCordyceps2,
    Stinkhorn_Acorn1,
    Stinkhorn_Acorn2,
    Stinkhorn_Acorn3,
    Stinkhorn_Acorn4,
    Stinkhorn_Acorn6,
    Stinkhorn_Acorn7,
    Stinkhorn_Acorn8,
    Stinkhorn_Acorn9,
    TreeSeat_Acorn0,
    TreeSeat_Acorn1,
    TreeSeat_Acorn2,
    TreeSeat_Acorn3,
    Fruitful_Acorn0,
    Fruitful_Acorn1,
    Fruitful_Acorn2,
    Fruitful_Acorn3,
}