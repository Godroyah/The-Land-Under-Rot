using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int loadingSceneIndex;
    public int sceneToLoadIndex;
    public int previousSceneIndex;

    private Scene loadingScene;
    private Scene sceneToLoad;
    
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
        //sceneToLoad = SceneManager.GetSceneByBuildIndex(sceneToLoadIndex);
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        if (!triggered)
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

        #region Loading Level

        Debug.Log("Loading Level");
        SceneManager.LoadScene(loadingSceneIndex, LoadSceneMode.Additive);

        loadingScene = SceneManager.GetSceneByName(SceneManager.GetSceneByBuildIndex(loadingSceneIndex).name);
        while (!loadingScene.isLoaded)
        {
            Debug.Log("NOT_YET_LOADED");
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("LOADED");
        SceneManager.SetActiveScene(loadingScene);

        // Protect assets from deletion
        this.gameObject.transform.parent = null;
        //GameController.Instance.transform.parent = null;
        SceneManager.MoveGameObjectToScene(this.gameObject, loadingScene);
        //SceneManager.MoveGameObjectToScene(GameController.Instance.gameObject, loadingScene);

        #endregion

        #region Unload

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(previousSceneIndex);

        while (!asyncUnload.isDone)
        {
            Debug.Log("NOT_YET_UNLOADED");
            //yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
        Debug.Log("UNLOADED");
        #endregion

        Debug.Log("The previous scene has been unloaded. Starting to load the next scene.");
        Time.timeScale = 1;

        #region SceneToLoad

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoadIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            //yield return null;
        }

        sceneToLoad = SceneManager.GetSceneByName(SceneManager.GetSceneByBuildIndex(sceneToLoadIndex).name); 
        SceneManager.SetActiveScene(sceneToLoad);

        // Protect assets from deletion
        //SceneManager.MoveGameObjectToScene(GameController.Instance.gameObject, sceneToLoad);

        #endregion

        // Unloading the LoadingScene
        SceneManager.UnloadSceneAsync(loadingScene);
        yield return null;


    }
}
