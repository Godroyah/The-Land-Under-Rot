using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayer : MonoBehaviour
{
    //Text textBox;
    TextMeshProUGUI textBox;
    public string fullText = "";
    public string displayedText;
    float textSpeed = 25f;

    public Dialogue myDialogue; // TODO: Find a way to find the parent cutscene
    Coroutine tempCoroutine;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();

        /* TODO: Figure out a better, less dependent search
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("@GameController").GetComponent<GameController>();
        }*/

        //myCutscene = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Cutscene>();

        myDialogue = gameObject.GetComponentInParent(typeof(Dialogue)) as Dialogue;

        fullText = textBox.text;
        displayedText = "";
    }

    private void Update()
    {
        if (myDialogue != null && myDialogue.dialogueManager != null)
        {
            textSpeed = myDialogue.dialogueManager.TextSpeed;
        }

        textBox.text = displayedText;
    }

    private void OnEnable()
    {
        //myCutscene = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Cutscene>(); // TODO: WTF is this

        //myCutscene.currentTextDisplayer = this;

        myDialogue = gameObject.GetComponentInParent(typeof(Dialogue)) as Dialogue;

        myDialogue.currentTextDisplayer = this;
        tempCoroutine = StartCoroutine(DisplayText());
    }

    private void OnDisable()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
        }

        if (textBox != null)
        {
            textBox.text = "";
        }
    }

    public void DisplayFullText()
    {
        StopCoroutine(tempCoroutine);
        displayedText = "";
        displayedText = fullText;
    }

    IEnumerator DisplayText()
    {
        displayedText = "";
        if(GameController.Instance != null)
        {
            while (GameController.Instance.dialogueManager == null)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        else
            yield return null;

        while (fullText.Length < 1)
        {
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            displayedText += fullText[i];

            if (fullText[i] == '.' || fullText[i] == '?' || fullText[i] == '!')
            {
                yield return new WaitForSeconds(1f / textSpeed);
            }
            else if (fullText[i] == ' ')
            {
                continue;
            }

            yield return new WaitForSeconds(1f / textSpeed);
        }

        myDialogue.hasFinishedDisplayingText = true;
        yield return null;
    }
}
