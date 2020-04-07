using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreeSeat_NPC { NONE, CATKIN, BUDDY, LIZARD, ROOTFORD, MS_STAMEN,
    EXIT_FENWAY, MULCH_FENWAY, PEDALTON, CARROT_SLUG, STRANGER}

//PEAPOD ^ ?

public class TreeSeat_NPC_Talk : Interactable
{
    public TreeSeat_NPC treeSeatNPC;

    public bool isIntroduced;
    public bool mulchantMet;
    public bool angelTreeAwake;

    public int randomTalk;

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

        if(GameController.Instance != null)
        {
            switch (treeSeatNPC)
            {
                case TreeSeat_NPC.NONE:
                    Debug.LogWarning("NPC not set!");
                    break;
                case TreeSeat_NPC.CATKIN:
                    StartCoroutine(Catkin());
                    break;
                case TreeSeat_NPC.BUDDY:
                    StartCoroutine(Buddy());
                    break;
                //case TreeSeat_NPC.PEAPOD:
                //    StartCoroutine(PeadPod());
                    //break;
                case TreeSeat_NPC.LIZARD:
                    StartCoroutine(Lizard());
                    break;
                case TreeSeat_NPC.ROOTFORD:
                    StartCoroutine(Rootford());
                    break;
                case TreeSeat_NPC.MS_STAMEN:
                    StartCoroutine(MsStamen());
                    break;
                case TreeSeat_NPC.EXIT_FENWAY:
                    StartCoroutine(ExitFenway());
                    break;
                case TreeSeat_NPC.MULCH_FENWAY:
                    StartCoroutine(MulchFenway());
                    break;
                case TreeSeat_NPC.PEDALTON:
                    StartCoroutine(Pedalton());
                    break;
                case TreeSeat_NPC.CARROT_SLUG:
                    StartCoroutine(CarrotSlug());
                    break;
                case TreeSeat_NPC.STRANGER:
                    StartCoroutine(Stranger());
                    break;
            }
        }
    }

    IEnumerator Catkin()
    {
        if(!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Catkin_Intro);
            isIntroduced = true;
        }
        else if (GameController.Instance.mulchant_GivenBottles && !GameController.Instance.angelTreeAwake && !mulchantMet)
        {
            dialogueManager.StartDialogue(Reply.CK_Spoke_To_Mulchant);
            mulchantMet = true;
        }
        else if(GameController.Instance.angelTreeAwake && !angelTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.CK_Tree_Is_Awake);
            angelTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 5);

            if(randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Catkin_Repeat_1);
            }
            else if(randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Catkin_Repeat_2);
            }
            else if (randomTalk == 3)
            {
                dialogueManager.StartDialogue(Reply.Catkin_Repeat_3);
            }
            else if (randomTalk == 4)
            {
                dialogueManager.StartDialogue(Reply.Catkin_Repeat_4);
            }
        }

        yield return null;
    }

    IEnumerator Buddy()
    {
       dialogueManager.StartDialogue(Reply.Buddy);
       yield return null;
    }

    //IEnumerator PeadPod()
    //{
    //    dialogueManager.StartDialogue(Reply.Peap


    //   yield return null;
    //}

        //Require separate script for yes/no functionality?

    IEnumerator Lizard()
    {
        Debug.Log("Grrr, Hiss, I don't say things yet.");

        //dialogueManager.StartDialogue(Reply

       yield return null;
    }

    IEnumerator Rootford()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.TS_Rootford_Intro);
            isIntroduced = true;
        }
        else
        {
            dialogueManager.StartDialogue(Reply.TS_Rootford_Repeat);
        }

        yield return null;
    }

    IEnumerator MsStamen()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Miss_Stamen_Intro);
            isIntroduced = true;
        }
        else if (GameController.Instance.mulchant_GivenBottles && !GameController.Instance.angelTreeAwake && !mulchantMet)
        {
            dialogueManager.StartDialogue(Reply.Miss_Stamen_Spoke_To_Mulchant);
            mulchantMet = true;
        }
        else if (GameController.Instance.angelTreeAwake && !angelTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Miss_Stamen_Tree_Is_Awake);
            angelTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 3);

            if (randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Miss_Stamen_Repeat_1);
            }
            else if (randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Miss_Stamen_Repeat_2);
            }
        }

        yield return null;
    }

    IEnumerator ExitFenway()
    {
        dialogueManager.StartDialogue(Reply.TS_Fenway_Exit);

       yield return null;
    }

    IEnumerator MulchFenway()
    {
       dialogueManager.StartDialogue(Reply.TS_Fenway_Mulch);

       yield return null;
    }

    IEnumerator Pedalton()
    {
        dialogueManager.StartDialogue(Reply.Pedalton);

       yield return null;
    }

    IEnumerator CarrotSlug()
    {
        if(!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Carrot_Slug_Intro);
            isIntroduced = true;
        }
        else
        {
            randomTalk = Random.Range(1, 5);

            if (randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Carrot_Slug_Repeat_1);
            }
            else if (randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Carrot_Slug_Repeat_2);
            }
            else if (randomTalk == 3)
            {
                dialogueManager.StartDialogue(Reply.Carrot_Slug_Repeat_3);
            }
            else if (randomTalk == 4)
            {
                dialogueManager.StartDialogue(Reply.Carrot_Slug_Repeat_4);
            }
        }
        yield return null;
    }

    IEnumerator Stranger()
    {
        if (!isIntroduced)
        {
            dialogueManager.StartDialogue(Reply.Stranger_Intro);
            isIntroduced = true;
        }
        else if (GameController.Instance.angelTreeAwake && !angelTreeAwake)
        {
            dialogueManager.StartDialogue(Reply.Stranger_Tree_Is_Awake);
            angelTreeAwake = true;
        }
        else
        {
            randomTalk = Random.Range(1, 4);

            if (randomTalk == 1)
            {
                dialogueManager.StartDialogue(Reply.Stranger_Repeat_1);
            }
            else if (randomTalk == 2)
            {
                dialogueManager.StartDialogue(Reply.Stranger_Repeat_2);
            }
            else if (randomTalk == 3)
            {
                dialogueManager.StartDialogue(Reply.Stranger_Repeat_3);
            }
        }

        yield return null;
    }
}
