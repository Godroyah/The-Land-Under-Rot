using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_GazeGrowth : GazeGrowth
{
    public GameObject cordysepBarrier;

    private Vector3 cordys_Start_Position;

    private Vector3 cordys_Lowered_Position;

    private float dropDistance;

    public float lowerDistance;

    public float cordyDelay;

    private float rateStorage;

    [SerializeField]
    private bool waitReturn;

    [Range(0f, 1.0f), Tooltip("Adjust speed of cordyceps lowering and raising.")]
    private float rateOfChange = 0.1f;

    private void Start()
    {
        thisDetector = GetComponent<SphereCollider>();

        if (cordysepBarrier != null)
        {
            waitReturn = false;
            rateStorage = rateOfChange;
            cordys_Start_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y, cordysepBarrier.transform.position.z);
            cordys_Lowered_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y - lowerDistance, cordysepBarrier.transform.position.z);
        }
    }

    public override void Interact()
    {
        if (cordysepBarrier != null)
        {
            base.Interact();

            animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
            animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);

            thisDetector.enabled = false;

            StartCoroutine(Fungi(0));
        }
    }

    IEnumerator Fungi(float waitTime)
    {

        rateOfChange = rateStorage;
        float iteration = rateOfChange;

        yield return new WaitForSeconds(cordyDelay);

        while (rateOfChange < 1.0f)
        {
            cordysepBarrier.transform.position = Vector3.Lerp(cordysepBarrier.transform.position, cordys_Lowered_Position, rateOfChange);
            rateOfChange += iteration;
            yield return null;
        }

        if (waitTime != 0)
        {
            StartCoroutine(DelayReturn(waitTime));
        }
    }

    IEnumerator DelayReturn(float waitTime)
    {
        waitReturn = true;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(ReturnCords());
    }

    IEnumerator ReturnCords()
    {
        rateOfChange = rateStorage;
        float iteration = rateOfChange;

        while (rateOfChange < 1.0f)
        {
            cordysepBarrier.transform.position = Vector3.Lerp(cordysepBarrier.transform.position, cordys_Start_Position, rateOfChange);
            rateOfChange += iteration;
            yield return null;
        }

        thisDetector.enabled = true;
        waitReturn = false;

        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }
}
