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
        base.Interact();

        if (GameController.Instance.dialogueManager != null)
            dialogueManager = GameController.Instance.dialogueManager;

        if (GameController.Instance != null)
        {
            // Select which dialogue to 'say'
            if (!GameController.Instance.bus_Called)
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
            else if (GameController.Instance.bus_Called && GameController.Instance.tutorial_HasTalked_Rootford_Intro2)
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
                playerController = other.GetComponentInParent<PlayerController>();
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
