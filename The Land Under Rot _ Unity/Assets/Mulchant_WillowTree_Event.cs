using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulchant_WillowTree_Event : Event_Type
{
    public Animator willowTreeAnim;
    public Animator mulchantStandInAnim;
    public SkinnedMeshRenderer interactableMulchant;
    public SkinnedMeshRenderer eventMulchant;
    public SpriteRenderer mulchantPrompt;

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
        interactableMulchant.enabled = false;
        mulchantPrompt.enabled = false;
        eventMulchant.enabled = true;

        yield return new WaitForSeconds(mulchantDelay);

        AudioManager.Instance.Play_Tree_EyeOpening();
        mulchantStandInAnim.SetTrigger("AwakenTree");

        yield return new WaitForSeconds(treeDelay);

        willowTreeAnim.SetTrigger("Wakeup");
        eventMulchant.enabled = false;
        mulchantPrompt.enabled = true;
        interactableMulchant.enabled = true;
        gameController.willowTreeAwake = true;

        //yield return new WaitForSeconds(endSceneDelay);
    }
}
