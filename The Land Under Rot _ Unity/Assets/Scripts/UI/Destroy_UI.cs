using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destroy_UI : MonoBehaviour
{
    public TextMeshProUGUI area_Title;

    public void SetName(string text)
    {
        area_Title.text = text;
        Debug.Log("Renamed");
    }

    public void DestroyTitle()
    {
        Destroy(gameObject);
    }
}
