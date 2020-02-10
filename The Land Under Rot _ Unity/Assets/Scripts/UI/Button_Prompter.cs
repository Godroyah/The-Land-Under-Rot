using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameObject buttonPromptUI;

    public GameObject textObject;

    public GameObject imageObject;

    public bool promptSet;

    public string npcText;

    public TextMeshProUGUI promptText;

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
            //promptText = buttonPromptUI.GetComponentInChildren<Text>();

            
            textObject.SetActive(false);
            imageObject.SetActive(false);
            promptText.text = npcText;
            
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
