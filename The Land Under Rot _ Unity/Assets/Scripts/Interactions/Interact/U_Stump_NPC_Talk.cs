using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum U_Stump_NPC { NONE, MULCHANT, WILLOW_TREE, SPUD, FENWAY_1, FENWAY_2}

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
                case U_Stump_NPC.FENWAY_1:
                    StartCoroutine(Fenway_1());
                    break;
                case U_Stump_NPC.FENWAY_2:
                    StartCoroutine(Fenway_2());
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
                //dialogueManager.StartDialogue(Reply.Mulchant_Intro_FF);
                isIntroduced = true;
            }
            else
            {
                //dialogueManager.StartDialogue(Reply.Mulchant_No_Mulch_FF);
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
               // dialogueManager.StartDialogue(Reply.Mulchant_Gathered_Mulch_FF);
                gameController.willowTreeAwake = true;
            }
            else
            {
                //dialogueManager.StartDialogue(Reply.Mulchant_Tree_Awake_FF);
            }
        }

        yield return null;
    }

    IEnumerator WillowTree()
    {
        yield return null;
    }
    //WillowTree Coroutine Here

    IEnumerator Spud()
    {
        if (!isIntroduced)
        {
            //dialogueManager.StartDialogue(Reply.Little_Blue_Intro);
            isIntroduced = true;
        }
        else if (gameController.willowTreeAwake && !willowTreeAwake)
        {
            //dialogueManager.StartDialogue(Reply.Little_Blue_Tree_Awake);
            willowTreeAwake = true;
        }
        else
        {
            //randomTalk = Random.Range(1, 3);

            //if (randomTalk == 1)
            //{
            //    dialogueManager.StartDialogue(Reply.Little_Blue_Repeat_1);
            //}
            //if (randomTalk == 2)
            //{
            //    dialogueManager.StartDialogue(Reply.Little_Blue_Repeat_2);
            //}

        }

        yield return null;
    }


    IEnumerator Fenway_1()
    {
        AudioManager.Instance.Play_Fenway();
       // dialogueManager.StartDialogue(Reply.One_Way_Bark_Fenway);

        yield return null;
    }

    IEnumerator Fenway_2()
    {
        AudioManager.Instance.Play_Fenway();
       // dialogueManager.StartDialogue(Reply.One_Way_Bark_Fenway);

        yield return null;
    }
}
