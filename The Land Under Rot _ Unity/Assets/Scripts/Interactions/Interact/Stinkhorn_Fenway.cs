using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stink_FenwayType { NONE, INTRO, RED_GAZEGROWTH, SECRETS}

public class Stinkhorn_Fenway : Interactable
{

    public Stink_FenwayType fenwayType;

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
            AudioManager.Instance.Play_Fenway();
            switch (fenwayType)
            {
                case Stink_FenwayType.NONE:
                    Debug.LogWarning("Please select a Fenway type!");
                    break;
                case Stink_FenwayType.INTRO:
                    StartCoroutine(IntroFenway());
                    break;
                case Stink_FenwayType.RED_GAZEGROWTH:
                    dialogueManager.StartDialogue(Reply.Red_GG_Fenway);
                    break;
                case Stink_FenwayType.SECRETS:
                    dialogueManager.StartDialogue(Reply.Secrets_Fenway);
                    break;
                default:
                    Debug.LogWarning("Fenway_Type Error.");
                    break;
            }
        }
    }
    IEnumerator IntroFenway()
    {
        if(!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Stink_Fenway_Intro);
            isIntroduced = true;
        }
        else
        {
            dialogueManager.StartDialogue(Reply.Stink_Fenway_Repeat);
        }

        yield return null;
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
