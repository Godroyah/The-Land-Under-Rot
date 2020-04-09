using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameController gameController;

    public Billboard_UI billboardUI;

    public GameObject textObject;

    public GameObject imageObject;

    public TextMeshProUGUI promptText;

    public bool tutorial;
    //If tutorial prompt; make this true;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("AssignCam", 1);
        //gameController = GameController.Instance;
        //billboardUI.billBoardCam = gameController.playerController.camControl.myCamera;
        //billboardUI.camTransform = billboardUI.billBoardCam.transform;

        //if (gameController == null)
        //{
        //    gameController = GameObject.Find("@GameController").GetComponent<GameController>();
        //    if (gameController != null)
        //    {
        //        billboardUI.billBoardCam = gameController.playerController.camControl.myCamera;
        //        billboardUI.camTransform = billboardUI.billBoardCam.transform;
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("GameController not active in scene!");
        //}

        GameController.Instance.updateCameras += UpdateCamera;

        if (textObject == null)
        {
            if(tutorial)
                Debug.LogWarning("Text TMP missing! If this is a Tutorial Button Prompt, please attach the Text TMP child to the textObject reference!");
        }
        else if(textObject != null)
        {
            textObject.SetActive(false);
        }
        if (imageObject == null)
        {
            Debug.LogWarning("Image missing! Please attach the Image child to the imageObject reference!");
        }
        else
        {
            imageObject.SetActive(false);
        }
    }

    /*
    void AssignCam()
    {
        gameController = GameController.Instance;
        billboardUI.billBoardCam = GameController.Instance.mainCamera;
        billboardUI.camTransform = billboardUI.billBoardCam.transform;
    }
    */

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            /*
            if (gameController == null)
            {
                billboardUI.camTransform = GameController.Instance.mainCamera.transform;
            }
            */
            if(textObject != null)
            {
                textObject.SetActive(true);
            }
            imageObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            if(textObject != null)
            {
                textObject.SetActive(false);
            }
            imageObject.SetActive(false);
        }
    }

    public void UpdateCamera(Camera newCamera)
    {
        billboardUI.billBoardCam = newCamera;
        billboardUI.camTransform = newCamera.transform;

    }
}
