using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelTree_NewAreas_Event : Event_Type
{
    public GameObject fruitEntranceBranches;
    public GameObject willowEntranceBranches;
    public GameObject treeSeatBossBranch;

    Vector3 fruitStartPos;
    Vector3 fruitLoweredPos;

    Vector3 willowStartPos;
    Vector3 willowLoweredPos;

    Vector3 angelBossBranchStartPos;
    Vector3 angelBossBranchLoweredPos;

    [Space(5)]

    [Header("Fruit Branch Parameters")]

    [Range(1f, 40f)]
    public float fruitEntranceDelay;
    [Range(1f, 60f)]
    public float fruitEntranceLowerDist;
    [Range(0.1f, 1.0f)]
    public float dropSpeedFruit;

    [Space(3)]

    [Header("Willow Branch Parameters")]

    [Range(1f, 40f)]
    public float willowEntranceDelay;
    [Range(1f, 60f)]
    public float willowEntranceLowerDist;
    [Range(0.1f, 1.0f)]
    public float dropSpeedWillow;

    [Space(3)]

    [Header("Angel Boss Branch Parameters")]

    [Range(1f, 40f)]
    public float treeSeatBossBranchDelay;
    [Range(1f, 60f)]
    public float treeSeatBossBranchLowerDist;
    [Range(0.1f, 1.0f)]
    public float dropSpeedAngelBoss;

    [Space(3)]

    [Header ("Speed Ceiling")]

    [Range(1.0f, 15.0f)]
    public float totalSpeed;

    [Space(3)]

    private float dropRecorderFruit;
    private float dropRecorderWillow;
    private float dropRecorderAngelBoss;

    // Start is called before the first frame update
    void Start()
    {
        dropRecorderFruit = dropSpeedFruit;
        dropRecorderWillow = dropSpeedWillow;
        dropRecorderAngelBoss = dropSpeedAngelBoss;

        fruitStartPos = new Vector3(fruitEntranceBranches.transform.position.x, fruitEntranceBranches.transform.position.y, fruitEntranceBranches.transform.position.z);
        fruitLoweredPos = new Vector3(fruitEntranceBranches.transform.position.x, fruitEntranceBranches.transform.position.y - fruitEntranceLowerDist, fruitEntranceBranches.transform.position.z);

        willowStartPos = new Vector3(willowEntranceBranches.transform.position.x, willowEntranceBranches.transform.position.y, willowEntranceBranches.transform.position.z);
        willowLoweredPos = new Vector3(willowEntranceBranches.transform.position.x, willowEntranceBranches.transform.position.y - willowEntranceLowerDist, willowEntranceBranches.transform.position.z);

        angelBossBranchStartPos = new Vector3(treeSeatBossBranch.transform.position.x, treeSeatBossBranch.transform.position.y, treeSeatBossBranch.transform.position.z);
        angelBossBranchLoweredPos = new Vector3(treeSeatBossBranch.transform.position.x, treeSeatBossBranch.transform.position.y - treeSeatBossBranchLowerDist, treeSeatBossBranch.transform.position.z);
    }

    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(RevealFruitfulForest());
        StartCoroutine(RevealUnderstump());
        StartCoroutine(RevealBoss());
    }

    IEnumerator RevealFruitfulForest()
    {
        dropSpeedFruit = dropRecorderFruit;
        float fruitIteration = dropSpeedFruit;

        yield return new WaitForSeconds(fruitEntranceDelay);

        AudioManager.Instance.Play_ClearingBranches();

        while (dropSpeedFruit < totalSpeed)
        {
            fruitEntranceBranches.transform.position = Vector3.Lerp(fruitEntranceBranches.transform.position, fruitLoweredPos, dropSpeedFruit * Time.deltaTime);
            dropSpeedFruit += fruitIteration * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RevealUnderstump()
    {
        dropSpeedWillow = dropRecorderWillow;
        float willowIteration = dropSpeedWillow;

        yield return new WaitForSeconds(willowEntranceDelay);

        //AudioManager.Instance.Play_ClearingBranches();
        //AudioManager.Instance.Play_MassiveRootsMoving();

        while (dropSpeedWillow < totalSpeed)
        {
            willowEntranceBranches.transform.position = Vector3.Lerp(willowEntranceBranches.transform.position, willowLoweredPos, dropSpeedWillow * Time.deltaTime);
            dropSpeedWillow += willowIteration * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RevealBoss()
    {
        //Debug.Log("Triggered");
       
        dropSpeedAngelBoss = dropRecorderAngelBoss;
        float angelBossIteration = dropSpeedAngelBoss;

        //yield return new WaitForSeconds(treeSeatBossBranchDelay);

        while (dropSpeedAngelBoss < totalSpeed)
        {
            treeSeatBossBranch.transform.position = Vector3.Lerp(treeSeatBossBranch.transform.position, angelBossBranchLoweredPos, dropSpeedAngelBoss * Time.deltaTime);
            dropSpeedAngelBoss += angelBossIteration * Time.deltaTime;
            yield return null;
        }
    }
}
