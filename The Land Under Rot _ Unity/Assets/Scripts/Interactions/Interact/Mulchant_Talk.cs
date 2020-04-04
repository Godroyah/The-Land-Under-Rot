using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulchant_Talk : Interactable
{
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
            // Select which dialogue to 'say'
            if(!GameController.Instance.hasBrownMulch)
            {
                if (!GameController.Instance.mulchant_GivenBottles)
                {
                    dialogueManager.StartDialogue(Reply.Mulchant_Intro);
                    GameController.Instance.mulchant_GivenBottles = true;
                }
                else
                {
                    dialogueManager.StartDialogue(Reply.Mulchant_No_Mulch);
                }
            }
            else
            {
                if(!GameController.Instance.angelTreeAwake)
                {
                    dialogueManager.StartDialogue(Reply.Mulchant_Gathered_Mulch_PreCutscene);
                    GameController.Instance.angelTreeAwake = true;
                }
                else
                {
                    dialogueManager.StartDialogue(Reply.Mulchant_Tree_Is_Awake);
                }
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
