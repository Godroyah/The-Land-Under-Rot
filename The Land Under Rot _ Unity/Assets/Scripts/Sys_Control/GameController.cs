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

    [Space(5)]
    public bool area_Tutorial = false;
    public bool bus_Called = false;
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
        Invoke("SetAcorns", 1);
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
            }
            else
                playerController.StopPlayer = false;
        }
        //if(isDead)
        //{
        //    Reset();
        //}

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
}
