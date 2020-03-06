using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    public EventSystem mainMenuEvents;
    //public Selectable [] thisButton;
    public GameObject currentButton;
    public Button thisButton;
    
    

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}

    // Start is called before the first frame update
    void Start()
    {
        currentButton = mainMenuEvents.currentSelectedGameObject;
        //if(thisButton.)
        //for (int i = 0; i < thisButton.Length; i++)
        //{
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(currentButton != mainMenuEvents.currentSelectedGameObject)
        {
            //StartCoroutine();
        }
        //if(thisButton.)
    }

    IEnumerator SwitchButton()
    {


        yield return null;
    }
}
