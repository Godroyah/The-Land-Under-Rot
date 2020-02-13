using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public List<GameObject> Dialogue_GameObjects;

    Dialogue[] dialogues;

    public Canvas myCanvas;

    public bool hasActiveDialogue = false;

    [Header("Scene Testing")]
    public GameObject TestScene;
    public bool isTestingScene = false;

    [Range(1, 45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 25;

    [Header("Music Stuff")]
    public AudioSource dialogueMusicAS;
    public AudioClip dialogueMusic;
    public bool dialogueMusicIsPlaying;
    public bool shouldTurnOffMusic;

    Dialogue dialogue;

    private void Awake()
    {
        /* TODO: Figure out a better, less dependent search
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            /* 
            GameController gameController = GameObject.FindGameObjectWithTag("@GameController").GetComponent<GameController>();
            if (gameController != null)
            {
                //gameController.cutsceneManager = this;
            }
            
            isTestingScene = false;
        }
    */
        myCanvas.worldCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new Dialogue[Dialogue_GameObjects.Count];

        for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        {
            dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
        }
        //  OR
        //StartCoroutine(ObtainCutsceneScripts());

        if (TestScene != null && isTestingScene)
        {
            StartDialogue(TestScene.GetComponent<Dialogue>().SceneName);
        }
        else
        {
            StartDialogue("Test");
        }
    }

    private void LateUpdate()
    {
        //Debug.Log(battleManager.isInBattle);
        if (hasActiveDialogue == false && dialogueMusicIsPlaying == true)
        {
            shouldTurnOffMusic = true;

            if (shouldTurnOffMusic == true)
            {
                dialogueMusicAS.enabled = false;
                dialogueMusicIsPlaying = false;
                shouldTurnOffMusic = false;
            }
        }
    }

    IEnumerator ObtainDialogueScripts()
    {
        for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        {
            dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public bool StartDialogue(string sceneName)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                if (dialogue.SceneName == sceneName)
                {
                    dialogue.StartScene();
                    //Debug.Log(sceneName);
                    hasActiveDialogue = true;
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("Unable to start new scene '" + sceneName + "' because another scene is still/already started");
        }

        return false;
    }
}
