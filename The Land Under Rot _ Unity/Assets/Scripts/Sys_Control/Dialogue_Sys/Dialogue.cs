using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dialogue : MonoBehaviour
{
    [Tooltip("Name of the Scene for the DialogueManager to Find")]
    public int frameIndex;
    public string SceneName;
    public GameObject[] Frames;
    [Space(5)]
    [Header("Frame Triggers for Event Cam Shots")]
    public bool[] shotChange;


    [Space(5)]
    [Header("Text Done?")]
    public bool textIsDone;

    [Space(5)]

    [Header("Emote ID")]
    public string NPC;
    EmoteCheck emoteCheck;
    CamControl camControl;
    DialogueCam dialogueCam;
    Event_Trigger eventTrigger;
    GameController gameController;
    [Space(10)]
    public DialogueManager dialogueManager;

    public TextDisplayer currentTextDisplayer;
    public bool hasFinishedDisplayingText = false;
    public bool isCamEventActive;

    private void Start()
    {
        //if(Emotes.Length != Frames.Length)
        //{
        //    Debug.LogWarning("Please match and assign emotes for each frame of dialogue!");
        //}

        camControl = GameObject.Find("Camera_Jig").GetComponent<CamControl>();
        dialogueCam = GameObject.Find("Dialogue_Cam").GetComponent<DialogueCam>();

        for (int i = 0; i < Frames.Length; i++)
        {
            //emoteChecks[i] = Frames[i].GetComponent<EmoteCheck>();
            //emoteChecks[i].dialogueManager = dialogueManager;
            Frames[i].SetActive(false);
        }

        if (isCamEventActive)
        {
            if (eventTrigger == null)
            {
                eventTrigger = GetComponent<Event_Trigger>();
                //Include LogWarning if no Event_Trigger component is included
            }
        }

        //if(shotChange.Length > 0)
        //{
        //    shotChange[shotChange.Length] = true;
        //}
    }

    public void StartScene()
    {

        if (!isCamEventActive)
        {
            StartCoroutine(Scene());
        }
        else if (isCamEventActive && eventTrigger.interactionStarted == false)
        {
            StartCoroutine(Scene());
        }
    }

    IEnumerator FindEmote()
    {
        for(int i = 0; i < dialogueManager.npcs.Length; i++)
        {
            if (NPC == dialogueManager.npcs[i].NPC)
            {
                if (emoteCheck.Emote == EmoteType.SLEEPING)
                {
                    dialogueManager.npcs[i].npcEmotes.Sleeping.Play();
                }
                else if (emoteCheck.Emote == EmoteType.WAITING)
                {
                    dialogueManager.npcs[i].npcEmotes.Waiting.Play();
                }
                else if (emoteCheck.Emote == EmoteType.EXCLAMATION)
                {
                    dialogueManager.npcs[i].npcEmotes.Exclamation.Play();
                }
                else if (emoteCheck.Emote == EmoteType.ANGRY)
                {
                    dialogueManager.npcs[i].npcEmotes.Angry.Play();
                }
                else if (emoteCheck.Emote == EmoteType.SHOCKED)
                {
                    dialogueManager.npcs[i].npcEmotes.Shocked.Play();
                }
                else if (emoteCheck.Emote == EmoteType.CONFUSED)
                {
                    dialogueManager.npcs[i].npcEmotes.Confused.Play();
                }
                else if (emoteCheck.Emote == EmoteType.DIZZY)
                {
                    dialogueManager.npcs[i].npcEmotes.Dizzy.Play();
                }
                else if (emoteCheck.Emote == EmoteType.HAPPY)
                {
                    dialogueManager.npcs[i].npcEmotes.Happy.Play();
                } 
            }
        }
        yield return null;
    }

    IEnumerator Scene()
    {
        //Debug.Log("Scene Started");

        if (dialogueManager != null)
        {
            //Debug.Log("Still working?");
            dialogueManager.hasActiveDialogue = true;
        }

        for (int i = 0; i < Frames.Length; i++)
        {
            frameIndex = i;
            //  Debug.Log("Depth Level 1");
            if (i != 0)
                Frames[i - 1].SetActive(false);
            Frames[i].SetActive(true);
            emoteCheck = Frames[i].GetComponent<EmoteCheck>();
            if(emoteCheck != null)
            {
                if (emoteCheck.play == true)
                {
                    StartCoroutine(FindEmote());
                }
            }

            yield return new WaitForEndOfFrame();

            // Wait while the text hasn't finished or the player interacts to finish the text
            yield return new WaitUntil(() => (Input.GetButtonDown("Interact") || (hasFinishedDisplayingText || currentTextDisplayer == null)));

            if (i == Frames.Length - 1)
            {
                textIsDone = true;
            }

            //      Debug.Log("Depth Level 3");
            if (/*Input.GetButtonDown("Interact") ||*/ hasFinishedDisplayingText || currentTextDisplayer == null)
            {
                
                hasFinishedDisplayingText = false;
                currentTextDisplayer = null;

                // Check if the Frame has a DialogueOption
                Frame tempFrame = Frames[i].GetComponent<Frame>();
                if (tempFrame != null)
                {
                    //Debug.Log("Unlocking Now!");
                    camControl.lockPosition = true;
                    Cursor.lockState = CursorLockMode.None;
                    //Debug.Log("Can You See Me?");
                    Cursor.visible = true;
                    //Debug.Log("Cursor Unlocked");
                    if(tempFrame.dialogueButtons.firstSelectedGameObject != tempFrame.firstButton)
                    {
                        tempFrame.dialogueButtons.firstSelectedGameObject = tempFrame.firstButton;
                    }
                    yield return new WaitWhile(() => tempFrame.Get_ShouldWait() == true);
                    //Debug.Log("Cursor Frozen");
                    camControl.lockPosition = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    if (tempFrame.Get_ShouldContinue() == false)
                    {
                        i = Frames.Length + 2;
                        break;
                    }
                    else
                        continue; // This skips the need to press the interact key again

                }
                else
                {
                    yield return new WaitUntil(() => Input.GetButtonDown("Interact"));
                }
                
                continue; // This continues to the next frame
            }
            else
            {
                currentTextDisplayer.DisplayFullText();
                //Wait for text to display full text
                yield return new WaitForSeconds(0.1f);

                // Check if the Frame has a DialogueOption
                Frame tempFrame = Frames[i].GetComponent<Frame>();
                if (tempFrame != null)
                {
                    Debug.Log("Unlocking Now!");
                    Cursor.lockState = CursorLockMode.None;
                    Debug.Log("Can You See Me?");
                    Cursor.visible = true;
                    Debug.Log("Cursor Unlocked");
                    yield return new WaitWhile(() => tempFrame.Get_ShouldWait() == true);
                    Debug.Log("Cursor Frozen");
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    //tempFrame.Reset_ShouldWait();

                    if (tempFrame.Get_ShouldContinue() == false)
                    {
                        i = Frames.Length + 2;
                        //tempFrame.Reset_ShouldWait();
                        break;
                    }
                    else
                    {
                        tempFrame.Reset_ShouldWait();
                        continue; // This skips the need to press the interact key again
                    }
                }
            }



            yield return new WaitUntil(() => Input.GetButtonDown("Interact"));


            #region OldCode
            //yield return new WaitForEndOfFrame();
            //while (true)
            //{
            ////    Debug.Log("Depth Level 2");
            //    // TODO: Extremely high polling number for user input
            //    yield return new WaitForSeconds(0.00001f);
            //    if (Input.GetButtonDown("Interact"))
            //    {
            //  //      Debug.Log("Depth Level 3");
            //        if (hasFinishedDisplayingText || currentTextDisplayer == null)
            //        {
            //            hasFinishedDisplayingText = false;
            //            currentTextDisplayer = null;
            //            break;
            //        }
            //        else
            //        {
            //            currentTextDisplayer.DisplayFullText();
            //            //Wait for text to display full text
            //            yield return new WaitForSeconds(0.1f);


            //            while (true)
            //            {
            //    //            Debug.Log("Depth Level 4");
            //                // TODO: Extremely high polling number for user input
            //                yield return new WaitForSeconds(0.00001f);
            //                if (Input.GetButtonDown("Interact"))
            //                {
            //      //              Debug.Log("Depth Level 5");
            //                    break;
            //                }
            //            }

            //            break;
            //        }

            //    }
            //}
            #endregion
        }

        hasFinishedDisplayingText = false;
        Frames[Frames.Length - 1].SetActive(false);

        //Calls on Event_Trigger to start a cam event
        //KNOWN BUG: Currently is causing the next (or last) frame of dialogue to repeat during cam event.
        //Debug.Log("Dialogue is done!");
        if (dialogueManager.prepCamEvent && isCamEventActive)
        {
            //dialogueManager.hasActiveDialogue = false;
            eventTrigger.InitiateEvent();
            yield return new WaitUntil(() => eventTrigger.GetEventCam().startScene == false);
        }

        if (dialogueManager != null)
        {
            dialogueManager.hasActiveDialogue = false;
        }

        dialogueCam.RestPosition();

        //Camera.main.orthographicSize = tempNum;
        yield return null;
    }
}

/*
// Check if the Frame has a DialogueOption
Frame tempFrame = Frames[i].GetComponent<Frame>();
                        if (tempFrame != null)
                        {
                            yield return new WaitWhile(() => tempFrame.Get_ShouldWait() == true);

                            if (tempFrame.Get_ShouldContinue() == false)
                            {
                                i = Frames.Length + 2;
                                break;
                            }

                        }
    */
