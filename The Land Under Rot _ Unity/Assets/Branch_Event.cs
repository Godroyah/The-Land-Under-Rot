using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch_Event : Event_Type
{
    public GameObject branch;

    Vector3 branchStartPos;
    Vector3 branchLoweredPos;

    [Space(5)]

    [Header("Fruit Branch Parameters")]

    [Range(1f, 40f)]
    public float branchDelay;
    [Range(1f, 60f)]
    public float branchLowerDist;
    [Range(0.1f, 1.0f)]
    public float dropSpeedBranch;

    [Space(3)]

    [Header("Speed Ceiling")]

    [Range(1.0f, 15.0f)]
    public float totalSpeed;

    [Space(3)]

    private float dropRecorderBranch;

    // Start is called before the first frame update
    void Start()
    {
        dropRecorderBranch = dropSpeedBranch;

        branchStartPos = new Vector3(branch.transform.position.x, branch.transform.position.y, branch.transform.position.z);
        branchLoweredPos = new Vector3(branch.transform.position.x, branch.transform.position.y - branchLowerDist, branch.transform.position.z);
    }

    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(DropBranch());
    }

    IEnumerator DropBranch()
    {
        dropSpeedBranch = dropRecorderBranch;
        float branchIteration = dropSpeedBranch;

        yield return new WaitForSeconds(branchDelay);

        //AudioManager.Instance.Play_ClearingBranches();
        //AudioManager.Instance.Play_MassiveRootsMoving();

        while (dropSpeedBranch < totalSpeed)
        {
            branch.transform.position = Vector3.Lerp(branch.transform.position, branchLoweredPos, dropSpeedBranch * Time.deltaTime);
            dropSpeedBranch += branchIteration * Time.deltaTime;
            yield return null;
        }
    }
}
