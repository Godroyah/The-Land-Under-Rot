using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong_Cam : MonoBehaviour
{

    [SerializeField]
    public bool startScene;

    Transform currentViewPoint;

    public Transform[] viewPoints;

    //public float[] sceneTime;
    public float sceneTime;
    public float currentTime;
    [SerializeField]
    private int currentScene;

    //public bool[] glideToShot;
    public bool glideToShot;

    public float transitionSpeed;

    // Start is called before the first frame update
    void Start()
    {

        currentTime = sceneTime;
        currentScene = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentViewPoint = viewPoints[currentScene];

        if(startScene)
        {
            if(currentScene < viewPoints.Length)
            {
                if (currentTime < 0)
                {
                    currentTime = sceneTime;
                    currentScene += 1;
                }
                currentTime -= Time.deltaTime;
            }
            else
            {
                if (currentTime > 0)
                {
                    startScene = false;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if(glideToShot)
        {
            transform.position = Vector3.Lerp(transform.position, currentViewPoint.position, transitionSpeed * Time.deltaTime);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentViewPoint.rotation.eulerAngles.x, transitionSpeed * Time.deltaTime),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentViewPoint.rotation.eulerAngles.y, transitionSpeed * Time.deltaTime),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentViewPoint.rotation.eulerAngles.z, transitionSpeed * Time.deltaTime));
        }
        else
        {
            transform.position = currentViewPoint.transform.position;
            transform.rotation = currentViewPoint.transform.rotation;
        }
    }
}
