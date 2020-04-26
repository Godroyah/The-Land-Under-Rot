using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart_Cam_Event : Event_Type
{
    //public Animator mineCartAnim;
    public GameObject cartMulch;
    public GameObject realMulch;

    public Animator cartAnim;

    public float moveSpeed;
    public float acceleration;
    //public float turnSpeed;
    public float tipSpeed;
    private int currentPoint;
    private float distToPoint;
    private Vector3 targetDirection;
    //public Transform tipDirection;
    //public Vector3 tipDirection;
    public Transform tipAxis;
    private Vector3 newDirection;
    private Quaternion targetRotation;

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
            moveSpeed += acceleration * Time.deltaTime;
            //if (distToPoint < 0.1f)
            //{
            //    //tipAxis.rotation = Quaternion.RotateTowards(tipAxis.rotation, tipDirection.rotation, step);
            //    targetRotation = Quaternion.LookRotation(tipDirection.position - tipAxis.position);
            //    float step = tipSpeed * Time.deltaTime;
            //    tipAxis.rotation = Quaternion.Lerp(tipAxis.rotation, targetRotation, step);
            //    cartMulch.SetActive(false);
            //    realMulch.SetActive(true);
            //}
        }
    }

    IEnumerator FollowTrack()
    {
        started = true;
        //AudioManager.Instance.

        targetDirection = wayPoint.position - tipAxis.position;
        distToPoint = Vector3.Distance(tipAxis.position, wayPoint.position);

        yield return new WaitForSeconds(2f);

        cartAnim.SetTrigger("Start_Rolling");

        while (distToPoint > 0.1f)
        {
            distToPoint = Vector3.Distance(tipAxis.position, wayPoint.position);
            targetDirection = wayPoint.position - tipAxis.position;
            //newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
            tipAxis.position = Vector3.MoveTowards(tipAxis.position, wayPoint.position, moveSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(newDirection);

            yield return null;
        }
       
        if(distToPoint < 0.1f)
        {
            cartAnim.SetTrigger("Tip_Over");
            realMulch.SetActive(true);
        }
    }
}
