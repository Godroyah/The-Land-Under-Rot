using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [Tooltip("Name of the Scene for the DialogueManager to Find")]
    public string SceneName;
    public GameObject Background;
    public GameObject[] Frames;

    GameController gameController;
    public DialogueManager dialogueManager;

    public TextDisplayer currentTextDisplayer;
    public bool hasFinishedDisplayingText = false;

    private void Awake()
    {
        GameObject temp = GameObject.Find("@DialogueManager");
        if (temp != null)
        {
            dialogueManager = temp.GetComponent<DialogueManager>();

            if (dialogueManager != null)
            {
                if (!dialogueManager.Dialogue_GameObjects.Contains(gameObject))
                    dialogueManager.Dialogue_GameObjects.Add(gameObject);
            }
            else
                Debug.LogWarning("Missing DialogueManager for dialogues to operate.");
        }


        if (Background == null)
            Background = new GameObject();
    }


    private void Start()
    {
        if (Background != null)
            Background.SetActive(false);

        for (int i = 0; i < Frames.Length; i++)
        {
            Frames[i].SetActive(false);
        }
    }

    public void StartScene()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        Debug.Log("Scene Started");

        // TODO: Similar camera-esc zoom for dialogue?
        //float tempNum = Camera.main.orthographicSize;
        //Camera.main.orthographicSize = 11.2f;

        Background.SetActive(true);

        for (int i = 0; i < Frames.Length; i++)
        {
            if (i != 0)
                Frames[i - 1].SetActive(false);

            Frames[i].SetActive(true);
            while (true)
            {
                // TODO: Extremely high polling number for user input
                yield return new WaitForSeconds(0.00001f);
                if (Input.GetButtonUp("Submit"))
                {
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
                            // TODO: Extremely high polling number for user input
                            yield return new WaitForSeconds(0.00001f);
                            if (Input.GetButtonUp("Submit"))
                            {
                                break;
                            }
                        }

                        break;
                    }

                }
            }
        }

        hasFinishedDisplayingText = false;
        Background.SetActive(false);
        Frames[Frames.Length - 1].SetActive(false);

        if (gameController != null)
        {
            dialogueManager.hasActiveDialogue = false;
        }

        //Camera.main.orthographicSize = tempNum;
        yield return null;
    }
}
