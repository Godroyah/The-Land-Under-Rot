using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCam : MonoBehaviour
{
    public PlayerController playerController;

    public Transform restPosition;

    Camera thisCamera;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        if(restPosition != null)
        {
            transform.position = restPosition.position;
            transform.rotation = restPosition.rotation;
        }
    }

    public void TalkPosition()
    {
        Debug.Log("Start Talking");
        if(playerController.currentTarget.dialogueViewPoint != null)
        {
            transform.position = playerController.currentTarget.dialogueViewPoint.position;
            transform.rotation = playerController.currentTarget.dialogueViewPoint.rotation;

            thisCamera.enabled = true;
        }
    }

    public void RestPosition()
    {
        Debug.Log("Stop Talking");
        if (playerController.currentTarget.dialogueViewPoint != null)
        {
            thisCamera.enabled = false;

            transform.position = restPosition.position;
            transform.rotation = restPosition.rotation;
        }
    }

}
