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
        settingsOption.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        settingsOption.SetActive(false);
    }

    public void StartGame()
    {
        if(testing)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
