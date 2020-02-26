using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    GameController gameController;
    DialogueManager dialogueManager;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
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

        billboard_UI.SetActive(false);
    }

    public override void Interact()
    {
        base.Interact();

        if (dialogue != null)
            dialogueManager.StartDialogue(dialogue);
        else
            dialogueManager.StartDialogue("Test");
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
