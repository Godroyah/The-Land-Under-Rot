using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SelfReport : MonoBehaviour
{
    //public enum Report_Type {PAUSE_MENU, QUIT_MENU, PAUSE_EVENTS, RESUME, QUIT}
    public enum Report_Type { NONE, BROWN, GREEN, YELLOW, ACORN}
    public Report_Type reportType;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;

        switch (reportType)
        {
            case Report_Type.NONE:
                Debug.LogWarning("You haven't picked a report type!");
                break;
            case Report_Type.BROWN:
                gameController.brownBottle = GetComponent<RawImage>();
                break;
            case Report_Type.GREEN:
                gameController.greenBottle = GetComponent<RawImage>();
                break;
            case Report_Type.YELLOW:
                gameController.yellowBottle = GetComponent<RawImage>();
                break;
            case Report_Type.ACORN:
                gameController.acornCount = GetComponent<TextMeshProUGUI>();
                break;
            default:
                break;
        }

        //switch (reportType)
        //{
        //    case Report_Type.PAUSE_MENU:
        //        GameController.Instance.pauseMenu = this.gameObject;
        //        break;
        //    case Report_Type.QUIT_MENU:
        //        GameController.Instance.quitOption = this.gameObject;
        //        break;
        //    case Report_Type.PAUSE_EVENTS:
        //        GameController.Instance.pauseEventSystem = this.gameObject;
        //        break;
        //    case Report_Type.RESUME:
        //        GameController.Instance.resumeButton = this.gameObject.GetComponent<Button>();
        //        break;
        //    case Report_Type.QUIT:
        //        GameController.Instance.quitButton = this.gameObject.GetComponent<Button>();
        //        break;
        //    default:
        //        break;
        //}
        //if(reportType == Report_Type.PAUSE_MENU || reportType == Report_Type.QUIT_MENU)
        //{
        //    gameObject.SetActive(false);
        //}
    }


}
