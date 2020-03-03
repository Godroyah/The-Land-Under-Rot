using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_UI : MonoBehaviour
{
    //-----WARNING! Make sure XYX Scale you parent the Button_Prompt to are equivalent! 
    //-----(ex. x = 1, y = 1, z = 1 OR x = 2, y = 2, z = 2, ETC.)

    private PlayerController playerController;
    //private GameController gameController;

    public Camera billBoardCam;
    public Transform camTransform;
    public Canvas promptCanvas;
    //private CamControl camControl;
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
            if (GameController.Instance != null)
                billBoardCam = GameController.Instance.playerController.camControl.myCamera;
            //else if(playerController != null)
            //    billBoardCam = playerController.camControl.myCamera;

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
            //imageObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Billboard UI image is missing! Please reconnect reference!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!forInteractable)
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
