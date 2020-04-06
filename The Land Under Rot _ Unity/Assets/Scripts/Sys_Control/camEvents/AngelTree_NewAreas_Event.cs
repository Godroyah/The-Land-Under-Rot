using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelTree_NewAreas_Event : Event_Type
{
    public GameObject fruitEntranceBranches;
    public GameObject willowEntranceBranches;
    public GameObject treaSeatBossBranch;

    [Range(1f,20f)]
    public float fruitEntranceDelay;
    [Range(1f, 20f)]
    public float willowEntranceDelay;
    [Range(1f, 20f)]
    public float treeSeatBossBranchDelay;

    [Range(1f, 20f)]
    public float fruitEntranceLowerDist;
    [Range(1f, 20f)]
    public float willowEntranceLowerDist;
    [Range(1f, 20f)]
    public float treeSeatBossBranchLowerDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
