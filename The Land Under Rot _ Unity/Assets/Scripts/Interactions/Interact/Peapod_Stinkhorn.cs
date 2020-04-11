using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peapod_Stinkhorn : Interactable
{
    public bool isIntroduced;

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

        if (GameController.Instance != null)
        {
            if(!isIntroduced)
            {
                dialogueManager.StartDialogue(Reply.SS_BusDriver_1);
                isIntroduced = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.SS_BusDriver_2_Repeat);
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
