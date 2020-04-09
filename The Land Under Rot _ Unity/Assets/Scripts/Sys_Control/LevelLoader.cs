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
            yield return new WaitForEndOfFrame();
        }

        SceneManager.SetActiveScene(loadingScene);

        // Move this loader to the loading scene so we can unload the previous scene
        this.gameObject.transform.parent = null;
        SceneManager.MoveGameObjectToScene(this.gameObject, loadingScene);

        #endregion

        #region SceneToLoad

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoadIndex, LoadSceneMode.Additive);
        //AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(previousSceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return new WaitForSeconds(2f);
            //yield return null;
        }

        Debug.Log(SceneManager.GetSceneByBuildIndex(sceneToLoadIndex).name + " has been loaded." +
            "Starting to Unload the Loading Scene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoadIndex));

        #endregion

        #region Unload

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(previousSceneIndex);

        while (!asyncUnload.isDone)
        {
            yield return new WaitForSeconds(2f);
            //yield return null;
        }

        SceneManager.UnloadSceneAsync(loadingScene);
        yield return null;

        #endregion
    }
}
