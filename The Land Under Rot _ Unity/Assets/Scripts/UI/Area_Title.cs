using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Area_Title : MonoBehaviour
{
    public string area_Title;
    public GameObject title_Canvas;
    public TextMeshProUGUI region_Text;
    bool hasPlayed = false;

    void ActivateTitleUI()
    {
        GameObject titleCard = Instantiate(title_Canvas);
        if(title_Canvas != null)
        {
            title_Canvas.GetComponent<Destroy_UI>().SetName(area_Title);
            //region_Text = title_Canvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
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

    public void DestroyTitle()
    {
        Destroy(gameObject);
    }

    public void SetName(string text)
    {
        region_Text.text = text;
    }

    //IEnumerator Title_Card()
    //{

    //}
}
