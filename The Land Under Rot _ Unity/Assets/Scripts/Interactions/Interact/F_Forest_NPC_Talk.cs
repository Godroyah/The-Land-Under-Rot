using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum F_Forest_NPC { NONE, MULCHANT, STAR_TREE, LITTLE_BLUE,
    BANAN, STRAWBERT, BARK_FENWAY, GREEN_GAZE_FENWAY }

public class F_Forest_NPC_Talk : Interactable
{
    public F_Forest_NPC fruitForest_NPC;

    public bool isIntroduced;
    public bool starTreeAwake;

    Animator mulchantAnim;

    public int randomTalk;

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
            switch (fruitForest_NPC)
            {
                case F_Forest_NPC.NONE:
                    Debug.LogWarning("NPC not set!");
                    break;
                case F_Forest_NPC.MULCHANT:
                    StartCoroutine(Mulchant());
                    break;
                case F_Forest_NPC.STAR_TREE:
                    StartCoroutine(StarTree());
                    break;
                case F_Forest_NPC.LITTLE_BLUE:
                    StartCoroutine(LittleBlue());
                    break;
                case F_Forest_NPC.BANAN:
                    StartCoroutine(Banan());
                    break;
                case F_Forest_NPC.STRAWBERT:
                    StartCoroutine(Strawbert());
                    break;
                case F_Forest_NPC.BARK_FENWAY:
                    StartCoroutine(BarkFenway());
                    break;
                case F_Forest_NPC.GREEN_GAZE_FENWAY:
                    StartCoroutine(GreenGazeFenway());
                    break;
            }
        }
    }

    IEnumerator Mulchant()
    {
        // Select which dialogue to 'say'
        if (!gameController.hasGreenMulch)
        {
            if (!isIntroduced)
            {
                dialogueManager.StartDialogue(Reply.Mulchant_Intro_FF);
                isIntroduced = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Mulchant_No_Mulch_FF);
            }
        }
        else
        {
            if (!gameController.starTreeAwake)
            {
                if(mulchantAnim != null)
                {
                    mulchantAnim.SetTrigger("GotMulch");
                }
                else
                {
                    Debug.LogWarning("Mulchant Animator not assigned!");
                }
                dialogueManager.StartDialogue(Reply.Mulchant_Gathered_Mulch_FF);
                gameController.starTreeAwake = true;
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Mulchant_Tree_Awake_FF);
            }
        }

        yield return null;
    }

    IEnumerator StarTree()
    {
        if (gameController.wormsInFruitful)
        {
            if (!gameController.starTreeAwake)
            {
                dialogueManager.StartDialogue(Reply.Star_Tree_Sleeping);
            }
            else
            {
                dialogueManager.StartDialogue(Reply.Star_Tree_Awake);
                gameController.wormsInFruitful = false;
            }
        }
        else
        {
            if(!isIntroduced)
            {
                dialogueManager.StartDialogue(Reply.Star_Tree_Worms_Gone);
                isIntroduced = true;
            }
            else
            {
                randomTalk = Random.Range(1, 3);

                if (randomTalk == 1)
                {
                    dialogueManager.StartDialogue(Reply.Star_Tree_Repeat_1);
                }
                else if (randomTalk == 2)
                {
                    dialogueManager.StartDialogue(Reply.Star_Tree_Repeat_2);
                }
            }
        }

        yield return null;
    }

    IEnumerator LittleBlue()
    {
        if(!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Little_Blue_Intro);
            isIntroduced = true;
        }
        else if(gameController.starTreeAwake && !starTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Little_Blue_Tree_Awake);
            starTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 3);

            if(randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Little_Blue_Repeat_1);
            }
            if(randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Little_Blue_Repeat_2);
            }

        }

        yield return null;
    }

    IEnumerator Banan()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Banan_Intro);
            isIntroduced = true;
        }
        else if (gameController.starTreeAwake && !starTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Banan_Tree_Awake);
            starTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 3);

            if (randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Banan_Repeat_1);
            }
            if (randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Banan_Repeat_2);
            }

        }
        yield return null;
    }

    IEnumerator Strawbert()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Strawbert_Intro);
            isIntroduced = true;
        }
        else if (gameController.starTreeAwake && !starTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Strawbert_Tree_Awake);
            starTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 3);

            if (randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Strawbert_Repeat_1);
            }
            if (randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Strawbert_Repeat_2);
            }

        }
        yield return null;
    }

    IEnumerator BarkFenway()
    {
        dialogueManager.StartDialogue(Reply.One_Way_Bark_Fenway);

        yield return null;
    }

    IEnumerator GreenGazeFenway()
    {
        dialogueManager.StartDialogue(Reply.Green_GG_Fenway);

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
