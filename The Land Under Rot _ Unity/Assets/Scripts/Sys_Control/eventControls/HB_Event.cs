using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HB_Event : Interactable
{
    public GameObject cinematicCamera;

    private Camera cameraComponent;

    private Event_Cam eventCamController;

    GameObject[] playerUI;

    public bool firstInteraction;
    public bool interactionStarted;
    public bool alwaysInteract;
    public bool sceneStarted;

    public bool canSound;
    //TODO: This is janky AF; fix later

    private void Start()
    {
        playerUI = new GameObject[GameObject.FindGameObjectsWithTag("Player_UI").Length];
        playerUI = GameObject.FindGameObjectsWithTag("Player_UI");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        eventCamController = cinematicCamera.GetComponent<Event_Cam>();
        cameraComponent = cinematicCamera.GetComponent<Camera>();

        cameraComponent.enabled = false;
        firstInteraction = true;
        interactionStarted = false;
        sceneStarted = false;


        if (canSound)
        {
            if (objPreferences != null && audioSource != null)
                audioSource.clip = objPreferences.headbutt_AudioClip;
        }
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
            eventCamController.startScene = true;
            interactionStarted = true;

            StartCoroutine(ShowTime());
        }

    }

    private void Update()
    {

        if (interactionStarted && !eventCamController.startScene && sceneStarted)
        {
            StartCoroutine(EndTime());
        }
    }

    IEnumerator ShowTime()
    {
        eventCamController.startScene = true;
        interactionStarted = true;
        sceneStarted = true;


        if (eventCamController.startScene && interactionStarted)
        {

            playerController.HorizontalInput = 0;
            playerController.VerticalInput = 0;
            playerController.HeadbuttInput = 0;

            playerController.eventActive = true;


            playerController.camControl.myCamera.enabled = false;

            foreach (GameObject ui in playerUI)
            {
                ui.SetActive(false);
            }

            playerController.camControl.enabled = false;
            cameraComponent.enabled = true;
        }

        yield return null;
    }


    IEnumerator EndTime()
    {

        cameraComponent.enabled = false;
        playerController.camControl.enabled = true;

        foreach (GameObject ui in playerUI)
        {
            ui.SetActive(true);
        }

        playerController.camControl.myCamera.enabled = true;

        playerController.eventActive = false;

        playerController.HorizontalInput = Input.GetAxis("Horizontal");
        playerController.VerticalInput = Input.GetAxis("Vertical");
        playerController.HeadbuttInput = Input.GetAxis("Headbutt");
        firstInteraction = false;
        sceneStarted = false;

        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }
}
