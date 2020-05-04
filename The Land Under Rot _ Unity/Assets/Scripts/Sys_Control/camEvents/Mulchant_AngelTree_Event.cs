using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulchant_AngelTree_Event : Event_Type
{
    public Animator treeAnim;
    public Animator mulchantStandInAnim;
    public SkinnedMeshRenderer eventMulchant;
    public GameObject mulchantBottle;
   
    [Range(1f, 20f)]
    public float mulchantDelay;
    [Range(1f, 20f)]
    public float treeDelay;
    //[Range(1f, 20f)]
    //public float endSceneDelay;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        eventMulchant.enabled = false;
    }

    public override void StartEvent()
    {
        base.StartEvent();

       StartCoroutine(TreeAwakening());
    }

    IEnumerator TreeAwakening()
    {
        eventMulchant.enabled = true;

        yield return new WaitForSeconds(mulchantDelay);

        AudioManager.Instance.Play_Tree_EyeOpening();
        mulchantStandInAnim.SetTrigger("AwakenTree");
        mulchantBottle.SetActive(true);

        yield return new WaitForSeconds(treeDelay);

        treeAnim.SetBool("Awake", true);
        mulchantBottle.SetActive(false);
        eventMulchant.enabled = false;
        gameController.angelTreeAwake = true;

        //yield return new WaitForSeconds(endSceneDelay);
    }
}
