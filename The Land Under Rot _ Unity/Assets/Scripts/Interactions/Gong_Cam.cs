using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong_Cam : MonoBehaviour
{
    //public Gong gongController;

    public bool startScene;

    Transform currentViewPoint;

    //public Transform[] viewPoints;

    public Shot[] shots;

    //public float[] sceneTime;
    //public float sceneTime;
    public float currentTime;

    private int currentScene;

    //public bool[] glideToShot;
    //public bool glideToShot;

    //public float transitionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //gongController = GetComponent<Gong>();
        currentScene = 0;
        currentTime = shots[currentScene].sceneTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shots[0] != null)
        //{
        //    shots[0].viewpoint = 

        //    shots[shots.Length - 1].viewpoint
        //}

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
            transform.position = shots[currentScene].viewpoint.position;
            transform.rotation = shots[currentScene].viewpoint.rotation;
        }
    }
}

[System.Serializable]
public class Shot
{
    public string name = "Empty String";
    public bool canGlide = false;
    public float sceneTime = 1f;
    public float transitionSpeed = 1f;
    public Transform viewpoint;
}
