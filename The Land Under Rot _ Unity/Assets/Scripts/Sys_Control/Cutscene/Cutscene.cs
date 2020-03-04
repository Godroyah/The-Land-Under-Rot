using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [Tooltip("Name of the Scene for the CutsceneManager to Find")]
    public string SceneName;
    public VideoPlayer video;

    public CutsceneManager cutsceneManager;

    public bool hasFinishedDisplaying = false;

    private void Awake()
    {
        video.enabled = false;

        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.CameraNearPlane;
        video.isLooping = false;
        video.targetCamera = Camera.main;

    }

    private void Start()
    {
        if (cutsceneManager == null)
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
        }

    }

    public void StartScene()
    {
        StartCoroutine(Scene());
        StartCoroutine(CheckForEndScene());
    }

    IEnumerator CheckForEndScene()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.GetButton("Interact"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return null;
    }
}
