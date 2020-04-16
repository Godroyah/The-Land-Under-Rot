﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [Tooltip("Name of the Scene for the DialogueManager to Find")]
    public string SceneName;
    public GameObject[] Frames;

    CamControl camControl;
    Event_Trigger eventTrigger;
    GameController gameController;
    public DialogueManager dialogueManager;

    public TextDisplayer currentTextDisplayer;
    public bool hasFinishedDisplayingText = false;
    public bool isCamEventActive;

    private void Start()
    {
        camControl = GameObject.Find("Camera_Jig").GetComponent<CamControl>();

        for (int i = 0; i < Frames.Length; i++)
        {
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
            //  Debug.Log("Depth Level 1");
            if (i != 0)
                Frames[i - 1].SetActive(false);
            Frames[i].SetActive(true);

            yield return new WaitForEndOfFrame();

            // Wait while the text hasn't finished or the player interacts to finish the text
            yield return new WaitUntil(() => (Input.GetButtonDown("Interact") || (hasFinishedDisplayingText || currentTextDisplayer == null)));

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
