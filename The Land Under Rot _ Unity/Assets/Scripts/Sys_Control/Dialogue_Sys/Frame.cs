using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public EventSystem dialogueButtons;
    public GameObject firstButton;

    public bool HasDialogueOption = true;
    private bool shouldWait = true;
    private bool shouldContinue = true;

    private Dialogue myDialogue;

    private void Start()
    {
        myDialogue = gameObject.transform.parent.GetComponent<Dialogue>();
        //dialogueButtons.firstSelectedGameObject = startButton;
    }

    public void ContinueDialogue()
    {
        shouldWait = false;
        Invoke("Reset_ShouldWait", 1);
    }

    public bool Get_ShouldWait()
    {
        return shouldWait;
    }

    public void Reset_ShouldWait()
    {
        shouldWait = true;
    }

    public bool Get_ShouldContinue()
    {
        return shouldContinue;
    }
}
