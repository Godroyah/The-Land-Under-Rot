using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [Tooltip("Name of the Scene for the CutsceneManager to Find")]
    public string SceneName;
    public GameObject Background;
    public GameObject[] Frames;

    GameController gameController;
    public CutsceneManager cutsceneManager;

    public TextDisplayer currentTextDisplayer;
    public bool hasFinishedDisplayingText = false;

    private void Awake()
    {
        /*TODO: Figure out a better, less dependent search
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }*/

        GameObject temp = GameObject.Find("@CutsceneManager");
        if (temp != null)
        {
            cutsceneManager = temp.GetComponent<CutsceneManager>();

            if (cutsceneManager == null)
                Debug.LogWarning("Missing CutsceneManger for cutscenes to operate.");
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
            cutsceneManager.hasActiveCutscene = false;
        }

        //Camera.main.orthographicSize = tempNum;
        yield return null;
    }
}
