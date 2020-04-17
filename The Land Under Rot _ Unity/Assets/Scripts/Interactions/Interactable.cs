using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected PlayerController playerController;
    protected ObjectPreferences objPrefs;
    protected AudioSource audioSource;
    protected DialogueManager dialogueManager;

    public Event_Trigger eventTrigger;

    public GameObject billboard_UI;

    public Transform dialogueViewPoint;

    //public bool isNPC;

    //Determines whether or not interactable can activate a cutscene and how the cutscene should be activated through this interactable
    public bool usesCamEvent;
    public bool headButtActivation;
    public bool dialogueActivation;
    public bool triggerActivation;
   

    private void Awake()
    {
        if (usesCamEvent && headButtActivation && eventTrigger == null)
        {
            eventTrigger = GetComponent<Event_Trigger>();
            if (eventTrigger == null)
            {
                Debug.LogWarning("This object is marked as using a Cam Event! Either mark 'usesCamEvent' as false or attach an Event_Trigger script to this object!");
            }
        }
        //if (!GetComponent<Rigidbody>())
        //    Debug.LogWarning("This interactable ("+ gameObject.name + ") does not have a Rigidbody!");

        //#region PlayerController Search
        //GameObject temp = GameObject.FindGameObjectWithTag("Player");
        //if (temp != null)
        //{
        //    playerController = temp.GetComponent<PlayerController>();
        //    if (playerController == null)
        //        Debug.LogWarning("Player does not have the 'PlayerController' script or something is additionally tagged 'Player'");
        //}
        //else
        //{
        //    Debug.LogWarning("Could not find PlayerController.");
        //}
        //#endregion

        objPrefs = GetComponent<ObjectPreferences>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log(gameObject.name + " has been interacted with.");


        //Interactable cutscene activation
        if (usesCamEvent)
        {
            if(headButtActivation)
            {
                eventTrigger.InitiateEvent();
            }
            else if (dialogueActivation)
            {
                Debug.Log("Prep scene");
                dialogueManager.prepCamEvent = true;
            }
            //else if(triggerActivation)
            //{

            //}
        }

        if (objPrefs != null && audioSource != null)
            audioSource.Play();
    }
}

