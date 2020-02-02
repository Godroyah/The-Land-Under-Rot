using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameObject buttonPromptUI;

    public bool NPC;

    public bool Sign;

    public bool Bark;

    public bool EyeGazer;

    private string objectTag;

    public string signText;

    public string npcText;

    public string barkText;

    public string eyeGazerText;

    private Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        objectTag = gameObject.tag;
        promptText = buttonPromptUI.GetComponentInChildren<Text>();
        buttonPromptUI.SetActive(false);
        if(NPC)
        {
            promptText.text = npcText;
        }
        else if(Sign)
        {
            promptText.text = signText;
        }
        else if(Bark)
        {
            promptText.text = barkText;
        }
        else if(EyeGazer)
        {
            promptText.text = eyeGazerText;
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            //Instantiate(buttonPromptUI, transform.position, transform.rotation);
            buttonPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            buttonPromptUI.SetActive(false);
        }
    }
}
