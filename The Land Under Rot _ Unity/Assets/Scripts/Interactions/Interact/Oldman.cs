using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldman : Interactable
{
    //DialogueManager dialogueManager;
    

    // Start is called before the first frame update
    void Start()
    {
        billboard_UI.SetActive(false);
    }

    public override void Interact()
    {
        if (GameController.Instance.dialogueManager != null)
            dialogueManager = GameController.Instance.dialogueManager;

        //Interact MUST come after dialogue manager call to ensure any camera events called word properly
        base.Interact();

        AudioManager.Instance.Play_Rootford();

        if (GameController.Instance != null)
        {
            AudioManager.Instance.Play_Rootford();
            // Select which dialogue to 'say'
            if (!GameController.Instance.tutorial_bus_Called)
            {
                if (!GameController.Instance.tutorial_HasTalked_Rootford_Intro1)
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_1);
                    GameController.Instance.tutorial_HasTalked_Rootford_Intro1 = true;
                }
                else if (!GameController.Instance.tutorial_HasTalked_Rootford_Intro2)
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_2);
                    GameController.Instance.tutorial_HasTalked_Rootford_Intro2 = true;
                }
                else
                {
                    dialogueManager.StartDialogue(Reply.SS_Rootford_Intro_3_Repeat);
                }
            }
            else if (GameController.Instance.tutorial_bus_Called/* && GameController.Instance.tutorial_HasTalked_Rootford_Intro2*/)
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
            {
                playerController = GameController.Instance.playerController;
                playerController.interactables.Add(this);
            }
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
