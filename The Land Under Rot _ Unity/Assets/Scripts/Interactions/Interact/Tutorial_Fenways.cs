using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tut_FenwayType { NONE, MOVEMENT, HEADBUTT, JUMP, GAZEGROWTH}

public class Tutorial_Fenways : Interactable
{

    public Tut_FenwayType fenwayType;

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

        

        if (GameController.Instance != null)
        {
            billboard_UI.SetActive(false);
            AudioManager.Instance.Play_Fenway();
            switch(fenwayType)
            {
                case Tut_FenwayType.NONE:
                    Debug.LogWarning("Please select a Fenway type!");
                    break;
                case Tut_FenwayType.MOVEMENT:
                    dialogueManager.StartDialogue(Reply.Tut_Fenway_Movement);
                    break;
                case Tut_FenwayType.HEADBUTT:
                    dialogueManager.StartDialogue(Reply.Tut_Fenway_Headbutt);
                    break;
                case Tut_FenwayType.JUMP:
                    dialogueManager.StartDialogue(Reply.Tut_Fenway_Jump);
                    break;
                case Tut_FenwayType.GAZEGROWTH:
                    dialogueManager.StartDialogue(Reply.Tut_Blue_GG_Fenway);
                    break;
                default:
                    Debug.LogWarning("Fenway_Type Error.");
                    break;
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
        {
            billboard_UI.SetActive(false);
            playerController.interactables.Remove(this);
        }
            
    }

}
