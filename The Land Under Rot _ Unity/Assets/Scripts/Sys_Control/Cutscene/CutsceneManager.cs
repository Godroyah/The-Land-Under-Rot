using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public List<GameObject> cutscene_GameObjects;

    Cutscene[] cutscenes;

    public PlayerController playerController;

    public bool hasActiveCutscene = false;

    [Header("Scene Testing")]
    public GameObject TestScene;
    public bool isTestingScene = false;

    [Range(1, 45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 25;

    [Header("Music Stuff")]
    public AudioSource cutsceneMusicAS;
    public AudioClip cutsceneMusic;
    public bool cutsceneMusicIsPlaying;
    public bool shouldTurnOffMusic;

    private void Awake()
    {
        for (int i = 0; i < cutscene_GameObjects.Count; i++)
        {
            cutscenes[i] = cutscene_GameObjects[i].GetComponent<Cutscene>();
            cutscenes[i].cutsceneManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.cutsceneManager = this;

        if (GameController.Instance.playerController != null)
            playerController = GameController.Instance.playerController;

        cutscenes = new Cutscene[cutscene_GameObjects.Count];

        
        //  OR
        //StartCoroutine(ObtainCutsceneScripts());

        if (TestScene != null && isTestingScene)
        {
            StartCutscene(TestScene.GetComponent<Cutscene>().SceneName);
        }
    }

    private void LateUpdate()
    {
        //Debug.Log(battleManager.isInBattle);
        if (hasActiveCutscene == false && cutsceneMusicIsPlaying == true)
        {
            shouldTurnOffMusic = true;

            if (shouldTurnOffMusic == true)
            {
                cutsceneMusicAS.enabled = false;
                cutsceneMusicIsPlaying = false;
                shouldTurnOffMusic = false;
            }
        }
    }

    IEnumerator ObtainCutsceneScripts()
    {
        for (int i = 0; i < cutscene_GameObjects.Count; i++)
        {
            cutscenes[i] = cutscene_GameObjects[i].GetComponent<Cutscene>();
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public bool StartCutscene(string sceneName)
    {
        //Debug.Log(sceneName);
        if (!hasActiveCutscene)
        {
            foreach (Cutscene cutscene in cutscenes)
            {
                if (cutscene.SceneName == sceneName)
                {
                    cutscene.StartScene();
                    //Debug.Log(sceneName);
                    return true;
                }
            }
        }
        else
        {
            Debug.LogWarning("Unable to start new scene '" + sceneName + "' because another scene is already started!");
        }

        return false;
    }
}
