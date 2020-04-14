using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LizardEvent : Event_Type
{
    
    public Animator lizardAnim;

    public float moveSpeed;
    public float turnSpeed;
    private int currentPoint;
    private float distToPoint;
    private Vector3 targetDirection;
    private Vector3 newDirection;

    public MeshCollider lizardCollider;
    public SkinnedMeshRenderer lizardRenderer;
    public SkinnedMeshRenderer peaPodRenderer;
    public MeshRenderer cartRenderer;

    public Transform[] wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        
        //currentPoint = 0;
        lizardCollider.enabled = false;
        lizardRenderer.enabled = false;
        peaPodRenderer.enabled = false;
        cartRenderer.enabled = false;

        //distToPoint = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
    }


    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(FollowTrack());
    }

    IEnumerator FollowTrack()
    {
        lizardRenderer.enabled = true;
        lizardCollider.enabled = true;
        peaPodRenderer.enabled = true;
        cartRenderer.enabled = true;
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
        lizardAnim.SetTrigger("End_Lizard");
        GameController.Instance.tutorial_bus_Called = true;

        yield return new WaitForSeconds(2f); // TODO: REMOVE ME

        //SceneManager.LoadScene(0);
    }

}
