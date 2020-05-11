using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject thingToRotate;
    public float speed = 1f;

    private void Start()
    {
        if (thingToRotate == null)
        {
            thingToRotate = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        thingToRotate.transform.RotateAround(thingToRotate.transform.position, thingToRotate.transform.up, speed * Time.deltaTime);
    }
}
