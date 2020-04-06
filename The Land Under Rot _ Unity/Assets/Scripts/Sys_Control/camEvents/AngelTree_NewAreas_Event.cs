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

    [Range(1f, 20f)]
    public float fruitEntranceDelay;
    [Range(1f, 20f)]
    public float fruitEntranceLowerDist;
    [Range(0.1f, 10.0f)]
    public float dropSpeedFruit;

    [Space(3)]

    [Header("Willow Branch Parameters")]

    [Range(1f, 20f)]
    public float willowEntranceDelay;
    [Range(1f, 20f)]
    public float willowEntranceLowerDist;
    [Range(0.1f, 10.0f)]
    public float dropSpeedWillow;

    [Space(3)]

    [Header("Angel Boss Branch Parameters")]

    [Range(1f, 20f)]
    public float treeSeatBossBranchDelay;
    [Range(1f, 20f)]
    public float treeSeatBossBranchLowerDist;
    [Range(0.1f, 10.0f)]
    public float dropSpeedAngelBoss;

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

        StartCoroutine(RevealNewAreas());
    }

    IEnumerator RevealNewAreas()
    {
        dropSpeedFruit = dropRecorderFruit;
        dropSpeedWillow = dropRecorderWillow;
        dropSpeedAngelBoss = dropRecorderAngelBoss;

        float fruitIteration = dropSpeedFruit;
        float willowIteration = dropSpeedWillow;
        float angelBossIteration = dropSpeedAngelBoss;

        yield return new WaitForSeconds(fruitEntranceDelay);

        while (dropSpeedFruit < 1.0f)
        {
            fruitEntranceBranches.transform.position = Vector3.Lerp(fruitEntranceBranches.transform.position, fruitLoweredPos, dropSpeedFruit);
            dropSpeedFruit += fruitIteration;
            yield return null;
        }

        yield return new WaitForSeconds(willowEntranceDelay);

        while (dropSpeedFruit < 1.0f)
        {
            willowEntranceBranches.transform.position = Vector3.Lerp(willowEntranceBranches.transform.position, willowLoweredPos, dropSpeedWillow);
            dropSpeedWillow += willowIteration;
            yield return null;
        }

        yield return new WaitForSeconds(treeSeatBossBranchDelay);

        while (dropSpeedFruit < 1.0f)
        {
            treeSeatBossBranch.transform.position = Vector3.Lerp(treeSeatBossBranch.transform.position, angelBossBranchLoweredPos, dropSpeedAngelBoss);
            dropSpeedAngelBoss += angelBossIteration;
            yield return null;
        }
    }
}
