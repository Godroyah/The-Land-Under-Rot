using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulchant_AngelTree_Event : Event_Type
{
    public Animator treeAnim;
    public Animator mulchantAnim;

    public GameObject eventMulchant;
    //or MeshRenderer?

    public float mulchantDelay;
    public float treeDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void StartEvent()
    {
        base.StartEvent();

       // StartCoroutine(TreeAwakening());
    }

    //IEnumerator TreeAwakening()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
