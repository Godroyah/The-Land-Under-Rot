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

    //public string npcText;

    public TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        if(textObject == null)
        {
            Debug.LogWarning("Text TMP missing! Please attach the Text TMP child to the textObject reference!");
        }
        else if(imageObject == null)
        {
            Debug.LogWarning("Image missing! Please attach the Image child to the imageObject reference!");
        }
        else
        {   
           textObject.SetActive(false);
           imageObject.SetActive(false);
           //promptText.text = npcText;
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            //Instantiate(buttonPromptUI, transform.position, transform.rotation);
            //buttonPromptUI.SetActive(true);
            textObject.SetActive(true);
            imageObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            //buttonPromptUI.SetActive(false);
            textObject.SetActive(false);
            imageObject.SetActive(false);
        }
    }
}
