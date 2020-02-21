using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Area_Title : MonoBehaviour
{
    public string area_Title;
    public GameObject title_Canvas;
    bool hasPlayed = false;

    void ActivateTitleUI()
    {
        GameObject titleCard = Instantiate(title_Canvas);
        if(title_Canvas != null)
        {
            titleCard.GetComponent<Destroy_UI>().SetName(area_Title);
        }
        hasPlayed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayed)
        {
            ActivateTitleUI();
        }
    }
}
