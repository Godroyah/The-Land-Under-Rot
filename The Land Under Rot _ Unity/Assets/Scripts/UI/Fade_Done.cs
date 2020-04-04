using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Done : MonoBehaviour
{

    public bool fadeOver;

    // Start is called before the first frame update
    void Start()
    {
        fadeOver = false;
    }

    public void FadeOver()
    {
        fadeOver = true;
    }
}
