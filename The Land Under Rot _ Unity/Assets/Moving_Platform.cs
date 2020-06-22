using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public Transform[] waypoints;
    //public Platform[] waypoints;
    public int currentPlatform = 0;
    Transform currentWaypoint;

    public float snap;
    public float transitionSpeed;
    public float hangTime;

    private float delayedStart;

    public bool automatic;
    
    // Start is called before the first frame update
    void Start()
    {
        if(waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
        snap = transitionSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position != currentWaypoint.position)
        {
            GlidePlatform();
        }
        else
        {
            UpdateWayPoint();
        }
    }

    void GlidePlatform()
    {
        Vector3 heading = currentWaypoint.position - transform.position;
        transform.position += (heading / heading.magnitude) * transitionSpeed * Time.deltaTime;
        if(heading.magnitude < snap)
        {
            transform.position = currentWaypoint.position;
            delayedStart = Time.time;
        }
    }
    void UpdateWayPoint()
    {
        if(automatic)
        {
            if(Time.time - delayedStart > hangTime)
            {
                NextWayPoint();
            }
        }
    }
    public void NextWayPoint()
    {
        currentPlatform++;
        if(currentPlatform >= waypoints.Length)
        {
            currentPlatform = 0;
        }
        currentWaypoint = waypoints[currentPlatform];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = null;
    }

    //private void LateUpdate()
    //{
    //            transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, waypoints[currentPlatform].transitionSpeed * Time.deltaTime);

    //            Quaternion currentAngle = Quaternion.Euler(
    //                Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentWaypoint.rotation.eulerAngles.x, waypoints[currentPlatform].transitionSpeed * Time.deltaTime),
    //                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentWaypoint.rotation.eulerAngles.y, waypoints[currentPlatform].transitionSpeed * Time.deltaTime),
    //                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentWaypoint.rotation.eulerAngles.z, waypoints[currentPlatform].transitionSpeed * Time.deltaTime));

    //            transform.rotation = currentAngle;
    //}

    //[System.Serializable]
    //public class Platform
    //{
    //    [Range(1f, 20f)]
    //    public float transitionSpeed;
    //    [Range(1f, 20f)]
    //    public float hangTime;
    //    public Transform waypoint;
    //}
}
