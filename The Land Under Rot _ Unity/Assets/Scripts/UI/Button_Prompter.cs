using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameObject billBoardUI;

    public GameObject textObject;

    public GameObject imageObject;

    public string npcText;

    public TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        if(billBoardUI != null)
        {
            
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
