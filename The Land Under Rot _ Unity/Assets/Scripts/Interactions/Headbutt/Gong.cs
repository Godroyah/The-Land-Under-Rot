using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong : Interactable
{
    GameController gameController;

    PlayerController playerController;

    public GameObject cinematicCamera;

    private Camera cameraComponent;

    private Gong_Cam gongCamController;

    private bool firstInteraction;

    private void Start()
    {
        gongCamController = cinematicCamera.GetComponent<Gong_Cam>();
        cameraComponent = cinematicCamera.GetComponent<Camera>();
        cameraComponent.enabled = false;
        firstInteraction = true;
        #region GameController Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();

            if (gameController == null)
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
            Debug.LogWarning("Could not find GameController.");

        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        //Play Gong Sound effect

        if(firstInteraction)
        {
            gongCamController.startScene = true;
            //StartCoroutine(ShowTime());
        }

    }

    private void Update()
    {
        if(playerController != null)
        {
            if (gongCamController.startScene)
            {
                playerController.enabled = false;
                playerController.camControl.myCamera.enabled = false;
                cameraComponent.enabled = true;
            }
            else
            {
                cameraComponent.enabled = false;
                playerController.camControl.myCamera.enabled = true;
                playerController.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            playerController = other.GetComponentInParent<PlayerController>();
            Interact();
            //gameController.playerController.headbuttables.Add(this);
        }
    }

    //IEnumerator ShowTime()
    //{
    //    playerController.enabled = false;
    //    playerController.camControl.myCamera.enabled = false;


    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Headbutt"))
    //    {
    //        gameController.playerController.headbuttables.Remove(this);
    //    }
    //}
}
