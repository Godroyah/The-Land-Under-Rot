using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameObject buttonPromptUI;

    public Transform textObject;

    public Transform imageObject;

    public bool promptSet;

    public bool NPC;

    public bool Sign;

    public bool Bark;

    public bool GazeGrowth;

    private string promptTag;

    public string signText;

    public string npcText;

    public string barkText;

    public string eyeGazerText;

    private Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        promptTag = "Button_Prompt";

        foreach(Transform child in transform)
        {
            if(child.tag == promptTag)
            {
                buttonPromptUI = child.gameObject;
            }
        }


        //------ If a the Button Prompt BillboardUI object is a child of this object, it will find it and execute the code below
        //------ Otherwise it will issue a warning that a Billboard UI prefab is needed as a child of this object --------
        if(buttonPromptUI != null)
        {
            promptSet = true;
        }

        
        if(promptSet)
        {
            //buttonPromptUI = transform.Find()
            //objectTag = gameObject.tag;

            //--------- Seeks out Text component for writing in desired message ----------
            promptText = buttonPromptUI.GetComponentInChildren<Text>();

            //------- Sets and initially disables image and text portions of the button prompt UI
            //buttonPromptUI.SetActive(false);
            textObject = buttonPromptUI.transform.GetChild(0);
            imageObject = buttonPromptUI.transform.GetChild(1);
            textObject.gameObject.SetActive(false);
            imageObject.gameObject.SetActive(false);


            // -------- Allows implementer to manually decide what text they want displayed for the button prompt--------
            if (NPC)
            {
                promptText.text = npcText;
            }
            else if (Sign)
            {
                promptText.text = signText;
            }
            else if (Bark)
            {
                promptText.text = barkText;
            }
            else if (GazeGrowth)
            {
                promptText.text = eyeGazerText;
            }
        }
        else
        {
            Debug.LogWarning("Button_Prompt Prefab not set as child! Please parent a Button_Prompt to this object!");
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            //Instantiate(buttonPromptUI, transform.position, transform.rotation);
            //buttonPromptUI.SetActive(true);
            textObject.gameObject.SetActive(true);
            imageObject.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            //buttonPromptUI.SetActive(false);
            textObject.gameObject.SetActive(false);
            imageObject.gameObject.SetActive(false);
        }
    }
}
