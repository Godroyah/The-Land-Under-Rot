using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [Tooltip("Name of the Scene for the DialogueManager to Find")]
    public string SceneName;
    public GameObject[] Frames;

    Event_Trigger eventTrigger;
    GameController gameController;
    public DialogueManager dialogueManager;

    public TextDisplayer currentTextDisplayer;
    public bool hasFinishedDisplayingText = false;
    public bool isCamEventActive;

    private void Start()
    {
        for (int i = 0; i < Frames.Length; i++)
        {
            Frames[i].SetActive(false);
        }

        if(isCamEventActive)
        {
            if(eventTrigger == null)
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
            while (true)
            {
            //    Debug.Log("Depth Level 2");
                // TODO: Extremely high polling number for user input
                yield return new WaitForSeconds(0.00001f);
                if (Input.GetButtonDown("Interact"))
                {
              //      Debug.Log("Depth Level 3");
                    if (hasFinishedDisplayingText || currentTextDisplayer == null)
                    {
                        hasFinishedDisplayingText = false;
                        currentTextDisplayer = null;
                        break;
                    }
                    else
                    {
                        currentTextDisplayer.DisplayFullText();
                        //Wait for text to display full text
                        yield return new WaitForSeconds(0.1f);
                        while (true)
                        {
                //            Debug.Log("Depth Level 4");
                            // TODO: Extremely high polling number for user input
                            yield return new WaitForSeconds(0.00001f);
                            if (Input.GetButtonDown("Interact"))
                            {
                  //              Debug.Log("Depth Level 5");
                                break;
                            }
                        }

                        break;
                    }

                }
            }
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
