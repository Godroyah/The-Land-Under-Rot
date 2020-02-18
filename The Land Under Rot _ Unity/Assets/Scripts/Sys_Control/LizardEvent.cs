﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardEvent : Event_Type
{
    public float moveSpeed;
    public float turnSpeed;
    private int currentPoint;
    private float distToPoint;
    private Vector3 targetDirection;
    private Vector3 newDirection;

    public MeshCollider lizardCollider;
    public MeshRenderer lizardRenderer;
    public Transform[] wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        //currentPoint = 0;
        lizardCollider.enabled = false;
        lizardRenderer.enabled = false;
        //distToPoint = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
    }


    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(FollowTrack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowTrack()
    {
        lizardRenderer.enabled = true;
        lizardCollider.enabled = true;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            targetDirection = wayPoints[i].position - transform.position;
            distToPoint = Vector3.Distance(transform.position, wayPoints[i].position);
            while (distToPoint > 0.1f)
            {
                distToPoint = Vector3.Distance(transform.position, wayPoints[i].position);
                targetDirection = wayPoints[i].position - transform.position;
                newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[i].position, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(newDirection);
                yield return null;
            }
            //currentPoint = i;
        }
    }

}
