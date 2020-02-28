using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Done : MonoBehaviour
{
    PlayerController playerController;
    //public bool fadeOver;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //fadeOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOver()
    {
        playerController.Reset();
       // fadeOver = true;
    }
}
