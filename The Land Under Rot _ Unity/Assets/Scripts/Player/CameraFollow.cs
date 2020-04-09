using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraMoveSpeed = 120f;
    public GameObject cameraFollowObj;
    Vector3 followPos;
    public float clampAngle = 80f;
    public float inputSensitivity = 150f;

    public GameObject cameraObj;
    public GameObject playerObj;

    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;

    public float mouseX;
    public float mouseY;

    public float finalInputX;
    public float finalInputZ;

    public float smoothX;
    public float smoothY;

    private float rotY = 0f;
    private float rotX = 0f;

    private void Start()
    {
        GameController.Instance.mainCamera = cameraObj.GetComponent<Camera>();

        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX += mouseY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        Transform target = cameraFollowObj.transform;

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
