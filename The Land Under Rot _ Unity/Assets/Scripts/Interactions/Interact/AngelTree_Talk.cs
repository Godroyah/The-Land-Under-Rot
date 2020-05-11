using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelTree_Talk : Interactable
{
    void Start()
    {
        billboard_UI.SetActive(false);
        doneTalking = true;
    }

    public override void Interact()
    {
        if (GameController.Instance.dialogueManager != null)
            dialogueManager = GameController.Instance.dialogueManager;

        //Interact MUST come after dialogue manager call to ensure any camera events called word properly
        base.Interact();

        if (GameController.Instance != null)
        {
            AudioManager.Instance.Play_WillowTree();
            billboard_UI.SetActive(false);
            // Select which dialogue to 'say'
            if(!GameController.Instance.revealNewAreas)
            {
                if (!GameController.Instance.angelTreeAwake)
                {
                    dialogueManager.StartDialogue(Reply.Sleepy_AngelTree);
                }
                else
                {
                    dialogueManager.StartDialogue(Reply.Awake_AngelTree);
                    GameController.Instance.revealNewAreas = true;
                }
            }
            else
            {
                dialogueManager.StartDialogue(Reply.AngelTree_PostCutscene);
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
