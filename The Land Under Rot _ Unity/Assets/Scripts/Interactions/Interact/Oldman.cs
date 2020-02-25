using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldman : Interactable
{
    public GameObject billboard;
    private Billboard_UI billboard_UI;

    GameController gameController;
    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        billboard_UI = billboard.GetComponent<Billboard_UI>();

        #region GameController/DialogueManager Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();
            if (gameController != null)
                dialogueManager = gameController.dialogueManager;
            else
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
        {
            // Direct Finding
            Debug.LogWarning("Could not find GameController. Will default to direct search.");
            temp = GameObject.Find("@DialogueManager");
            if (temp != null)
            {
                dialogueManager = temp.GetComponent<DialogueManager>();
                if (dialogueManager == null)
                    Debug.LogWarning("@DialogueManager does not have the 'DialogueManager' script!");
            }
        }
        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        if (gameController != null)
        {
            // Select which dialogue to 'say'
            if (!gameController.bus_Called)
            {
                if (!gameController.tutorial_HasTalked_Rootford_Intro1)
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_1);
                    gameController.tutorial_HasTalked_Rootford_Intro1 = true;
                }
                else if (!gameController.tutorial_HasTalked_Rootford_Intro2)
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_2);
                    gameController.tutorial_HasTalked_Rootford_Intro2 = true;
                }
                else
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_3_Repeat);
                }
            }
            else
            {
                dialogueManager.StartDialogue(Reply.SS_Rootford_Bus_1_Repeat);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            if (playerController == null)
                playerController = other.GetComponentInParent<PlayerController>();
            else
                playerController.interactables.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
            playerController.interactables.Remove(this);
    }
}
