using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsOption;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValue;

    GameController gameController;

    //-----------------------------------------------------------------

    public bool testing;

    //-----------------------------------------------------------------
    //Only for testing both Main and Pause Menu in same scene;
    //Turn this off on MainMenuHolder AND GameController gameobjects
    //once MainMenu has its own scene

    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeScale != 1)
        Time.timeScale = 1;

        gameController = GameController.Instance;
        gameController.sensitivityBar = sensitivitySlider;
        sensitivitySlider.onValueChanged.AddListener(delegate { gameController.SensitivityValueCheckMain(); });
        gameController.mainMenu = GetComponent<MainMenu>();
    }

    public void Settings()
    {
        AudioManager.Instance.Play_UI_Click_MainMenu();
        settingsOption.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        AudioManager.Instance.Play_Stinger_Back_MainMenu();
        mainMenu.SetActive(true);
        settingsOption.SetActive(false);
    }

    public void StartGame()
    {
        AudioManager.Instance.Play_Stinger_Start_MainMenu();
        if (testing)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            //SceneManager.LoadScene((int)BuildOrder.CutsceneScene);

            //TODO: JANK
            GameObject GO_Loader = new GameObject();
            LevelLoader loader = GO_Loader.AddComponent<LevelLoader>();
            loader.sceneToLoadIndex = BuildOrder.CutsceneScene;
            loader.currentSceneIndex = BuildOrder.StartScreen;
            loader.LoadScene();
        }
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play_UI_Click_MainMenu();
        Debug.Log("Quit!");
        Application.Quit();
    }
}
