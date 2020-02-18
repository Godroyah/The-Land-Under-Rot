using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_UI : MonoBehaviour
{
    //-----WARNING! Make sure XYX Scale you parent the Button_Prompt to are equivalent! 
    //-----(ex. x = 1, y = 1, z = 1 OR x = 2, y = 2, z = 2, ETC.)

    private PlayerController playerController;
    private GameController gameController;

    public Camera billBoardCam;
    public Transform camTransform;
    public Canvas promptCanvas;
    private CamControl camControl;
    public GameObject textObject;
    public GameObject imageObject;

    public bool forInteractable;
    public bool tutorial;

    Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        if(forInteractable)
        {
            if (gameController == null)
            {
                gameController = GameObject.Find("@GameController").GetComponent<GameController>();
                
                if (gameController == null)
                {
                    playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
                }
            }
            else
            {
                Debug.LogWarning("GameController not active in scene!");
            }

            if (gameController != null)
                billBoardCam = gameController.playerController.camControl.myCamera;
            else if(playerController != null)
                billBoardCam = playerController.camControl.myCamera;
            else if(gameController == null && playerController == null)
            {
                Debug.LogWarning("Player and GameController both missing! Must AT LEAST have Player present in scene!");
            }

            camTransform = billBoardCam.transform;
            promptCanvas.worldCamera = billBoardCam;
        }
        

        
        startRotation = transform.rotation;

        if (textObject != null)
        {
            textObject.SetActive(false);
        }
        else if(tutorial)
        {
            Debug.LogWarning("Text TMP is missing from Tutorial Billboard UI! Please reconnect reference!");
        }
        if(imageObject != null)
        {
            imageObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Billboard UI image is missing! Please reconnect reference!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!forInteractable && camControl == null)
        {
            camTransform = billBoardCam.transform;
            promptCanvas.worldCamera = billBoardCam;
        }
        //----------------------------------------------------------------------------------------
        //-------Multiply the start rotation of Button Prompt with that of the camera-------------
        transform.rotation = camTransform.rotation * startRotation;
        //----------------------------------------------------------------------------------------
        //-------This will keep the UI facing the camera at all times (Billboarding)
    }
}
