using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Camera myCamera;
    public GameObject target;
    public Transform rotationTarget;

    private bool invertPitch = false;
    public float sensitivity_X = 10f;
    public float sensitivity_Y = 10f;
    private float horizontalInput;
    private float verticalInput;

    public Vector2 pitchMinMax = new Vector2(-30, 85);
    public float acceleration = .12f;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        if (myCamera == null)
        {
            Debug.LogWarning("No assigned camera found. Defaulting to MAIN");
            myCamera = Camera.main;
        }

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        currentRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        transform.position = target.transform.position;


        // Seems like over correction to me
        // JK THIS IS HIGHLY NEEDED
        rotationTarget.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Mouse X");
        verticalInput = Input.GetAxisRaw("Mouse Y");
    }

    private void Move()
    {
        // <<-----------------------------------------------------------------------------**
        // Get input data and clamp 
        // **---------------------------------------------**

        yaw += Input.GetAxis("Mouse X") * sensitivity_X;

        if (invertPitch)
        {
            pitch += Input.GetAxis("Mouse Y") * sensitivity_Y;
        }

        else
        {
            pitch += Input.GetAxis("Mouse Y") * -sensitivity_Y;
        }

        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Calculate rotation and apply to camera
        // **---------------------------------------------**

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, acceleration* Time.deltaTime);

        transform.eulerAngles = currentRotation;

        // **----------------------------------------------------------------------------->>
    }
}
