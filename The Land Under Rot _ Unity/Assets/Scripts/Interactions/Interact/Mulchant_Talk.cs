using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulchant_Talk : Interactable
{
    public Animator mulchantAnim;
    public SkinnedMeshRenderer mulchantRenderer;
    CapsuleCollider mulchantCollider;
    GameController gameController;

    void Start()
    {
        //mulchantAnim = GetComponent<Animator>();
        gameController = GameController.Instance;
        mulchantCollider = GetComponent<CapsuleCollider>();

        billboard_UI.SetActive(false);

        if(gameController.starTreeAwake && gameController.willowTreeAwake)
        {
            if(mulchantRenderer != null)
            {
                mulchantRenderer.enabled = false;
                mulchantCollider.enabled = false;
            }
        }
        else
        {
            if (mulchantRenderer != null)
            {
                mulchantRenderer.enabled = true;
                mulchantCollider.enabled = true;
            }
        }

    }

    public override void Interact()
    {
        if (GameController.Instance.dialogueManager != null)
            dialogueManager = GameController.Instance.dialogueManager;

        //Interact MUST come after dialogue manager call to ensure any camera events called word properly
        base.Interact();

        if (GameController.Instance != null)
        {
            AudioManager.Instance.Play_Mulchant();
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
                    if(mulchantAnim != null)
                    {
                        mulchantAnim.SetTrigger("GotMulch");
                    }
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
