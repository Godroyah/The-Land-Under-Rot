using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Prompter : MonoBehaviour
{
    public GameObject buttonPromptUI;

    // Start is called before the first frame update
    void Start()
    {
        buttonPromptUI.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
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
