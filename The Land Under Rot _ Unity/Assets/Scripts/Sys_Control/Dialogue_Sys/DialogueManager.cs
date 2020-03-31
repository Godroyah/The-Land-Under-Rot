using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public List<GameObject> Dialogue_GameObjects;

    Dialogue[] dialogues;

    public Canvas myCanvas;

    public bool prepCamEvent;
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

    private bool activeDialogueEvent;

    Dialogue dialogue;

    private void Awake()
    {
        myCanvas.worldCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.dialogueManager = this;

        dialogues = new Dialogue[Dialogue_GameObjects.Count];

        for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        {
            dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
            dialogues[i].dialogueManager = this;
        }
        //  OR
        //StartCoroutine(ObtainCutsceneScripts());

        if (TestScene != null && isTestingScene)
        {
            StartDialogue(TestScene.GetComponent<Dialogue>().SceneName);
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

    public bool StartDialogue(Reply sceneName)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                if (dialogue.SceneName == sceneName.ToString())
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

    public bool StartDialogue(Dialogue dialogue)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            dialogue.StartScene();
        }
        else
        {
            Debug.Log("Unable to start new scene '" + dialogue.SceneName + "' because another scene is still/already started");
        }

        return false;
    }



}
public enum Reply
{
    SS_Rootford_Intro_1,
    SS_Rootford_Intro_2,
    SS_Rootford_Intro_3_Repeat,
    SS_Rootford_Bus_1_Repeat,
    SS_BusDriver_1,
    SS_BusDriver_2_Repeat,
    MrPots_Sign,
    Test
}