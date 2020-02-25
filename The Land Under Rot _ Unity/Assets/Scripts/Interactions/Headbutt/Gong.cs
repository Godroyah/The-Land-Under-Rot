using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong : Interactable
{
    GameController gameController;

    public GameObject cinematicCamera;

    private Camera cameraComponent;

    private Gong_Cam gongCamController;

    GameObject[] playerUI;

    public bool firstInteraction;
    public bool interactionStarted;
    public bool alwaysInteract;

    public bool canSound;
    //TODO: This is janky AF; fix later

    private void Start()
    {
        playerUI = new GameObject[GameObject.FindGameObjectsWithTag("Player_UI").Length];
        playerUI = GameObject.FindGameObjectsWithTag("Player_UI");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        gongCamController = cinematicCamera.GetComponent<Gong_Cam>();
        cameraComponent = cinematicCamera.GetComponent<Camera>();

        cameraComponent.enabled = false;
        firstInteraction = true;
        interactionStarted = false;


        if(canSound)
        {
            if (objPreferences != null && audioSource != null)
                audioSource.clip = objPreferences.headbutt_AudioClip;
        }
        

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

        // Moved 'PlaySound' to the base Interactable
        // bc each obj will play a sound if they have one
        // regardless of the object. And if they don't have one 
        // assigned then they won't play a sound 

        if (firstInteraction || alwaysInteract)
        {
            gongCamController.startScene = true;
            interactionStarted = true;

            //StartCoroutine(ShowTime());
        }

    }

    private void Update()
    {
        if (playerController != null && (firstInteraction || alwaysInteract))
        //&& firstInteraction
        {
            if (gongCamController.startScene && interactionStarted)
            {

                playerController.enabled = false;
                playerController.camControl.myCamera.enabled = false;

                foreach (GameObject ui in playerUI)
                {
                    ui.SetActive(false);
                }

                playerController.camControl.enabled = false;
                cameraComponent.enabled = true;
            }
            else if (interactionStarted && !gongCamController.startScene)
            {
                cameraComponent.enabled = false;
                playerController.camControl.enabled = true;

                foreach (GameObject ui in playerUI)
                {
                    ui.SetActive(true);
                }

                playerController.camControl.myCamera.enabled = true;
                playerController.enabled = true;
                firstInteraction = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
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
