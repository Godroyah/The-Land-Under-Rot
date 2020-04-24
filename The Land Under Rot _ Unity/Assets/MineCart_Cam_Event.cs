using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart_Cam_Event : Event_Type
{
    public Animator mineCartAnim;

    public float moveSpeed;
    public float turnSpeed;
    public float tipSpeed;
    private int currentPoint;
    private float distToPoint;
    private Vector3 targetDirection;
    public Transform tipDirection;
    private Vector3 newDirection;

    bool started;

    //public Transform[] wayPoints;

    public Transform wayPoint;

    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(FollowTrack());
    }

    private void Update()
    {
        if(started)
        {
            moveSpeed += 0.1f * Time.deltaTime;
        }
    }

    IEnumerator FollowTrack()
    {
        started = true;
        //AudioManager.Instance.

        targetDirection = wayPoint.position - transform.position;
        distToPoint = Vector3.Distance(transform.position, wayPoint.position);
        while (distToPoint > 0.1f)
        {
            distToPoint = Vector3.Distance(transform.position, wayPoint.position);
            targetDirection = wayPoint.position - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(newDirection);

            yield return null;
        }

        //    for (int i = 0; i < wayPoints.Length; i++)
        //{
        //    targetDirection = wayPoints[i].position - transform.position;
        //    distToPoint = Vector3.Distance(transform.position, wayPoints[i].position);
        //    while (distToPoint > 0.1f)
        //    {
        //        distToPoint = Vector3.Distance(transform.position, wayPoints[i].position);
        //        targetDirection = wayPoints[i].position - transform.position;
        //        newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
        //        transform.position = Vector3.MoveTowards(transform.position, wayPoints[i].position, moveSpeed * Time.deltaTime);
        //        transform.rotation = Quaternion.LookRotation(newDirection);
        //        yield return null;
        //    }
            if(transform.position == wayPoint.position)
            {
                Vector3.RotateTowards(transform.right, tipDirection.position, tipSpeed * Time.deltaTime, 0.0f);
            }
            //currentPoint = i;
        //}
        //lizardAnim.SetTrigger("End_Lizard");
    }
}
