using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue_Reader : MonoBehaviour
{
    private Text dialogueInput;

    public GameObject continueIcon;
    public GameObject stopIcon;

    public string[] dialogueLines;
    //might need to put this on the interactable script instead

    public float characterSpeed = 0.15f;
    public float charSpeedUp = 0.2f;
    //Lower charSpeedUp is actually faster

    //Test Input
    public KeyCode dialogueSpeedUp = KeyCode.Return;

    private bool isDialogueSpelling = false;
    private bool isDialogueActive = false;
    private bool isDialogueDone = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueInput = GetComponent<Text>();
        dialogueInput.text = "";

        HideIcons();
    }

    // Update is called once per frame
    void Update()
    {
        //Test input only
        //if(Input.GetMouseButton(0))
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!isDialogueActive)
            {
                isDialogueActive = true;
                StartCoroutine(BeginDialogue());
                //StartCoroutine(DisplayDialogue(dialogueLines[0]));
            }
        }
    }

    private IEnumerator BeginDialogue()
    {
        int dialogueLength = dialogueLines.Length;
        int currentCharIndex = 0;

        while(currentCharIndex < dialogueLength || isDialogueSpelling)
        {
            if(!isDialogueSpelling)
            {
                isDialogueSpelling = true;
                StartCoroutine(DisplayDialogue(dialogueLines[currentCharIndex++]));

                if(currentCharIndex >= dialogueLength)
                {
                    isDialogueDone = true;
                }
            }

            yield return 0;
        }

        while (true)
        {
            if(Input.GetKeyDown(dialogueSpeedUp))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

        isDialogueDone = false;
        isDialogueActive = false;
    }

    private IEnumerator DisplayDialogue(string display)
    {
        int dialogueLength = display.Length;
        int currentCharIndex = 0;

        HideIcons();

        dialogueInput.text = "";

        while (currentCharIndex < dialogueLength)
        {
            dialogueInput.text += display[currentCharIndex];
            currentCharIndex++;

            if(currentCharIndex < dialogueLength)
            {
                if(Input.GetKey(dialogueSpeedUp))
                {
                    yield return new WaitForSeconds(characterSpeed * charSpeedUp);
                }
                else
                {
                    yield return new WaitForSeconds(characterSpeed);
                }
            }
            else
            {
                break;
            }
        }

        ShowIcon();

        while(true)
        {
            if(Input.GetKeyDown(dialogueSpeedUp))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

        isDialogueSpelling = false;
        dialogueInput.text = "";
    }

    void HideIcons()
    {
        continueIcon.SetActive(false);
        stopIcon.SetActive(false);
    }

    void ShowIcon()
    {
        if(isDialogueDone)
        {
            stopIcon.SetActive(true);
            return;
        }
        continueIcon.SetActive(true);
    }
}
