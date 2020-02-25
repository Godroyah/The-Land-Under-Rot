using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : Interactable
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

        dialogueManager.StartDialogue("Testing");
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
