using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_UI : MonoBehaviour
{
    public Transform camTransform;

    Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(camTransform.rotation.x, camTransform.rotation.y, 0);
        //transform.rotation = Quaternion.LookRotation(-camTransform.forward, camTransform.up);
        transform.rotation = camTransform.rotation * startRotation;
    }
}
