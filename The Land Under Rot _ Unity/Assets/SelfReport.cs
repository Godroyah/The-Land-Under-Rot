using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelfReport : MonoBehaviour
{
    public enum Report_Type {PAUSE_MENU, QUIT_MENU, PAUSE_EVENTS, RESUME, QUIT}
    public Report_Type reportType;

    // Start is called before the first frame update
    void Start()
    {
        switch (reportType)
        {
            case Report_Type.PAUSE_MENU:
                GameController.Instance.pauseMenu = this.gameObject;
                break;
            case Report_Type.QUIT_MENU:
                GameController.Instance.quitOption = this.gameObject;
                break;
            case Report_Type.PAUSE_EVENTS:
                GameController.Instance.pauseEventSystem = this.gameObject;
                break;
            case Report_Type.RESUME:
                GameController.Instance.resumeButton = this.gameObject.GetComponent<Button>();
                break;
            case Report_Type.QUIT:
                GameController.Instance.quitButton = this.gameObject.GetComponent<Button>();
                break;
            default:
                break;
        }
        if(reportType == Report_Type.PAUSE_MENU || reportType == Report_Type.QUIT_MENU)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
