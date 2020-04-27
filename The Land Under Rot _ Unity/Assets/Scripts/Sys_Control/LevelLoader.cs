using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public BuildOrder sceneToLoadIndex;
    public BuildOrder currentSceneIndex;
    public Transform returnSpawn;
    [Space(5)]
    public bool isDisabled = false;

    private Scene loadingScene;
    private Scene sceneToLoad;

    private bool triggered = false;

    private float deltaTimeLoading = 0f;
    private float minDeltaTimeLoading = 5f;


    public void LoadScene()
    {
        if (!triggered)
        {
            triggered = true;

            //SceneManager.LoadSceneAsync(sceneToLoad.buildIndex, LoadSceneMode.Additive);
            //StartCoroutine(CheckForLevelLoaded());

            StartCoroutine(LoadYourAsyncScene());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && !isDisabled)
        {
            triggered = true;

            //SceneManager.LoadSceneAsync(sceneToLoad.buildIndex, LoadSceneMode.Additive);
            //StartCoroutine(CheckForLevelLoaded());

            StartCoroutine(LoadYourAsyncScene());
        }
    }

    /*
    IEnumerator CheckForLevelLoaded()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

        }
    }
    */

    IEnumerator LoadYourAsyncScene()
    {
        StartCoroutine(Timer());
        #region Loading Level

        //Debug.Log("Loading Level");
        SceneManager.LoadScene((int)BuildOrder.LoadingLevel, LoadSceneMode.Additive);

        //loadingScene = SceneManager.GetSceneByName(SceneManager.GetSceneByBuildIndex((int)BuildOrder.LoadingLevel).name);
        loadingScene = SceneManager.GetSceneByBuildIndex((int)BuildOrder.LoadingLevel);
        while (!loadingScene.isLoaded)
        {
            //Debug.Log("NOT_YET_LOADED");
            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("LOADED");
        SceneManager.SetActiveScene(loadingScene);

        // Protect assets from deletion
        this.gameObject.transform.parent = null;
        GameController.Instance.transform.parent = null;
        SceneManager.MoveGameObjectToScene(this.gameObject, loadingScene);
        SceneManager.MoveGameObjectToScene(GameController.Instance.gameObject, loadingScene);

        #endregion

        GameController.Instance.SaveGame();

        #region Unload

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync((int)currentSceneIndex);

        while (!asyncUnload.isDone)
        {
            //Debug.Log("NOT_YET_UNLOADED");
            //yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
        //Debug.Log("UNLOADED");
        #endregion

        //Debug.Log("The previous scene has been unloaded. Starting to load the next scene.");
        Time.timeScale = 1;

        #region SceneToLoad

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)sceneToLoadIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            //yield return null;
        }

        sceneToLoad = SceneManager.GetSceneByName(SceneManager.GetSceneByBuildIndex((int)sceneToLoadIndex).name);
        SceneManager.SetActiveScene(sceneToLoad);

        // Protect assets from deletion
        SceneManager.MoveGameObjectToScene(GameController.Instance.gameObject, sceneToLoad);

        LevelLoader[] foundLevelLoaders = new LevelLoader[5];
        foundLevelLoaders = FindObjectsOfType<LevelLoader>();
        foreach (LevelLoader loader in foundLevelLoaders)
        {
            if (loader == null)
                continue; // incase the previous scene has reminates

            if (loader.sceneToLoadIndex == this.currentSceneIndex && loader.currentSceneIndex == this.sceneToLoadIndex)
            {
                if (loader.returnSpawn != null)
                {
                    GameController.Instance.playerController.transform.parent.position = loader.returnSpawn.position;
                }

                break;
            }
        }


        #endregion

        yield return new WaitWhile(() => deltaTimeLoading <= minDeltaTimeLoading);

        // Unloading the LoadingScene
        SceneManager.UnloadSceneAsync(loadingScene);
        yield return null;


    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            deltaTimeLoading += Time.deltaTime;
        }
    }
}

public enum BuildOrder
{
    StartScreen,
    CutsceneScene,
    TutorialArea,
    StinkhornStop,
    TreeSeat,
    FruitfulForest,
    Understump,
    BossLevel,
    LoadingLevel
}
