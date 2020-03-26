using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Cam : MonoBehaviour
{
    public bool startScene;

    Transform currentViewPoint;

    public Shot[] shots;

    public float currentTime;

    private int currentScene;

    private Vector3 restingPoint;

    // Start is called before the first frame update
    void Start()
    {
        restingPoint = Vector3.zero;
        currentScene = 0;
        currentTime = shots[currentScene].sceneTime;

        Invoke("Setup", 1);
    }

    void Setup()
    {
        if (shots[0].viewpoint == null)
        {
            shots[0].viewpoint = GameController.Instance.playerController.camControl.myCamera.transform;
        }
        if (shots[shots.Length - 1].viewpoint == null)
        {
            shots[shots.Length - 1].viewpoint = shots[0].viewpoint;
        }

        if (shots[0] != null)
        {
            foreach (Shot shot in shots)
            {
                if (shot.sceneTime < 1.0f)
                {
                    shot.sceneTime = 3.0f;
                }
                if (shot.transitionSpeed < 1.0f)
                {
                    shot.transitionSpeed = 1.0f;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!startScene)
        {
            currentScene = 0;
            currentTime = shots[currentScene].sceneTime;
            transform.position = restingPoint;
        }
        //TODO: Kinda jank just for reuse of Red GazeGrowth

        currentViewPoint = shots[currentScene].viewpoint;
        //Debug.Log(shots.Length);

        if (startScene)
        {

            if (currentTime < 0)
            {
                if (currentScene != shots.Length - 1)
                {
                    currentScene += 1;
                }
                currentTime = shots[currentScene].sceneTime;
                if(shots[currentScene].sceneTime <= 0)
                {
                    Debug.LogWarning("Scene Time for shot" + currentScene + " is too close to 0! Please increase to a minimum of 1!");
                }
                if(shots[currentScene].isEventTrigger )
                {
                    if(shots[currentScene].eventType != null)
                    {
                        shots[currentScene].eventType.StartEvent();
                    }
                }
            }
            currentTime -= Time.deltaTime;

            if (currentTime < 0 && currentScene >= shots.Length - 1)
            {
                //gongController.firstInteraction = false;
                startScene = false;
            }

        }
    }

    private void LateUpdate()
    {
        if (startScene)
        {
            if (shots[currentScene].canGlide)
            {
                transform.position = Vector3.Lerp(transform.position, currentViewPoint.position, shots[currentScene].transitionSpeed * Time.deltaTime);

                Quaternion currentAngle = Quaternion.Euler(
                    Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentViewPoint.rotation.eulerAngles.x, shots[currentScene].transitionSpeed * Time.deltaTime),
                    Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentViewPoint.rotation.eulerAngles.y, shots[currentScene].transitionSpeed * Time.deltaTime),
                    Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentViewPoint.rotation.eulerAngles.z, shots[currentScene].transitionSpeed * Time.deltaTime));

                transform.rotation = currentAngle;
            }
            else
            {
                transform.position = currentViewPoint.transform.position;
                transform.rotation = currentViewPoint.transform.rotation;
            }
        }
        else
        {
            if(shots[0].viewpoint != null)
            {
                transform.position = shots[currentScene].viewpoint.position;
                transform.rotation = shots[currentScene].viewpoint.rotation;
            }
        }
    }
}

[System.Serializable]
public class Shot
{
    public string name = "Empty String";
    public bool canGlide = false;
    public bool isEventTrigger = false;
    public bool isDialogueEvent = false;
    //public bool hasTriggerTime = false;
    public Event_Type eventType;
    [Range(1f, 20f)]
    public float sceneTime = 1f;
    [Range(1f, 20f)]
    public float transitionSpeed = 1f;
    public Transform viewpoint;
}
