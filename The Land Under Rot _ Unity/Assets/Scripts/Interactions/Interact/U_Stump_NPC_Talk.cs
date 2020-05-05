using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum U_Stump_NPC { NONE, MULCHANT, WILLOW_TREE, SPUD, FENWAY_YELLOWGAZE}

public class U_Stump_NPC_Talk : Interactable
{

    public U_Stump_NPC understump_NPC;

    bool isIntroduced;
    bool willowTreeAwake;

    public Animator mulchantAnim;

    int randomTalk;

    GameController gameController;

    void Start()
    {
        billboard_UI.SetActive(false);
        gameController = GameController.Instance;
    }

    public override void Interact()
    {
        if (gameController.dialogueManager != null)
            dialogueManager = gameController.dialogueManager;

        //Interact MUST come after dialogue manager call to ensure any camera events called word properly
        base.Interact();
        billboard_UI.SetActive(false);

        if (gameController != null)
        {
            switch (understump_NPC)
            {
                case U_Stump_NPC.NONE:
                    Debug.LogWarning("NPC not set!");
                    break;
                case U_Stump_NPC.MULCHANT:
                    StartCoroutine(Mulchant());
                    break;
                case U_Stump_NPC.WILLOW_TREE:
                    StartCoroutine(WillowTree());
                    break;
                case U_Stump_NPC.SPUD:
                    StartCoroutine(Spud());
                    break;
                case U_Stump_NPC.FENWAY_YELLOWGAZE:
                    StartCoroutine(Fenway_YellowGaze());
                    break;
            }
        }
    }

    IEnumerator Mulchant()
    {
        AudioManager.Instance.Play_Mulchant();
        // Select which dialogue to 'say'
        if (!gameController.hasYellowMulch)
        {
            if (!isIntroduced)
            {
                dialogueManager.StartDialogue(Reply.Mulchant_Intro_US);
                isIntroduced = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Mulchant_No_Mulch_US);
            }
        }
        else
        {
            if (!gameController.willowTreeAwake)
            {
                if (mulchantAnim != null)
                {
                    mulchantAnim.SetTrigger("GotMulch");
                }
                else
                {
                    Debug.LogWarning("Mulchant Animator not assigned!");
                }
                dialogueManager.StartDialogue(Reply.Mulchant_Gathered_Mulch_US);
                gameController.willowTreeAwake = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Mulchant_Tree_Awake_US);
            }
        }

        yield return null;
    }

    IEnumerator WillowTree()
    {
        if (!gameController.underStumpLightsOn)
        {
            if (!gameController.willowTreeAwake)
            {
                dialogueManager.StartDialogue(Reply.Willow_Tree_Asleep);
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Willow_Tree_Awake);
                gameController.underStumpLightsOn = true;
            }
        }
        else
        {
            if (!isIntroduced)
            {
                dialogueManager.StartDialogue(Reply.Willow_Tree_Post_Cutscene);
                isIntroduced = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Willow_Tree_Repeat_1);
            }
        }

        yield return null;
    }
    //WillowTree Coroutine Here

    IEnumerator Spud()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Spud_Intro);
            isIntroduced = true;
        }
        else if (gameController.willowTreeAwake && !willowTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Spud_Tree_Awake);
            willowTreeAwake = true;
        }
        else
        {
            dialogueManager.StartDialogue(Reply.Spud_Repeat_1);
        }

        yield return null;
    }


    IEnumerator Fenway_YellowGaze()
    {
        AudioManager.Instance.Play_Fenway();
       dialogueManager.StartDialogue(Reply.Yellow_GG_Fenway);

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
        {
            billboard_UI.SetActive(false);
            playerController.interactables.Remove(this);
        }
            
    }
}
