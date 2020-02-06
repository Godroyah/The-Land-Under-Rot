using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    Text textBox;
    public string fullText = "";
    public string displayedText;
    float textSpeed = 25f;

    Cutscene myCutscene;
    Coroutine tempCoroutine;

    GameController gameController;

    private void Awake()
    {
        textBox = GetComponent<Text>();

        /* TODO: Figure out a better, less dependent search
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("@GameController").GetComponent<GameController>();
        }*/

        myCutscene = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Cutscene>();

        fullText = textBox.text;
        displayedText = "";
    }

    void Start()
    {

    }

    private void Update()
    {
        if (gameController != null && gameController.cutsceneManager != null)
        {
            textSpeed = gameController.cutsceneManager.TextSpeed;
        }

        textBox.text = displayedText;
    }

    private void OnEnable()
    {
        myCutscene = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Cutscene>(); // TODO: WTF is this

        myCutscene.currentTextDisplayer = this;
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
        while (gameController != null && gameController.cutsceneManager == null)
        {
            yield return new WaitForEndOfFrame();
        }

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

            yield return new WaitForSeconds(1f / textSpeed);
        }

        myCutscene.hasFinishedDisplayingText = true;
        yield return null;
    }
}
