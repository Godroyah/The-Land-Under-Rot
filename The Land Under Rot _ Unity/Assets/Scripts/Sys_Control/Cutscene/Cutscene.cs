using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [Tooltip("Name of the Scene for the CutsceneManager to Find")]
    public string SceneName;
    public VideoPlayer video;

    public CutsceneManager cutsceneManager;

    public bool hasFinishedDisplaying = false;

    private void Awake()
    {
        GameObject temp = GameObject.Find("@CutsceneManager");
        if (temp != null)
        {
            cutsceneManager = temp.GetComponent<CutsceneManager>();

            if (cutsceneManager != null)
            {
                if (!cutsceneManager.cutscene_GameObjects.Contains(gameObject))
                    cutsceneManager.cutscene_GameObjects.Add(gameObject);
            }
            else
                Debug.LogWarning("Missing CutsceneManager for cutscenes to operate.");
        }

        video.enabled = false;

        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.CameraNearPlane;
        video.isLooping = false;
        video.targetCamera = Camera.main;

    }

    public void StartScene()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        Debug.Log("Scene Started");

        if (cutsceneManager.playerController != null)
            cutsceneManager.playerController.enabled = false;

        cutsceneManager.hasActiveCutscene = true;
        video.enabled = true;

        video.Play();

        yield return new WaitForSeconds((float)video.length);

        hasFinishedDisplaying = true;
        video.enabled = false;
        cutsceneManager.hasActiveCutscene = false;


        if (cutsceneManager.playerController != null)
            cutsceneManager.playerController.enabled = true;

        yield return null;
    }
}
