using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_UI : MonoBehaviour
{
    //-----WARNING! Make sure XYX Scale you parent the Button_Prompt to are equivalent! 
    //-----(ex. x = 1, y = 1, z = 1 OR x = 2, y = 2, z = 2, ETC.)
    public Canvas promptCanvas;
    public Transform camTransform;

    Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        promptCanvas.GetComponent<Canvas>().worldCamera = Camera.main;

        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------------------------------------------------------------------
        //-------Multiply the start rotation of Button Prompt with that of the camera-------------
        transform.rotation = camTransform.rotation * startRotation;
        //----------------------------------------------------------------------------------------
        //-------This will keep the UI facing the camera at all times (Billboarding)
    }
}
