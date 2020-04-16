using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartDriver : Interactable
{
    //public GameObject billboard;
    //private Billboard_UI billboardUI;

    GameController gameController;
    //DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        //billboardUI = billboard.GetComponent<Billboard_UI>();

        gameController = GameController.Instance;
        dialogueManager = gameController.dialogueManager;
    }

    public override void Interact()
    {
        base.Interact();


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
