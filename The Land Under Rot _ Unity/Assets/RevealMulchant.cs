using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMulchant : Interactable
{
    public SkinnedMeshRenderer mulchantRenderer;
    SphereCollider eventDetector;
    public SphereCollider bossBlocker;
    CapsuleCollider mulchantCollider;

    bool eventTripped;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        eventDetector = GetComponent<SphereCollider>();
        mulchantCollider = GetComponent<CapsuleCollider>();

        if(gameController.starTreeAwake && gameController.willowTreeAwake)
        {
            eventDetector.enabled = true;
            bossBlocker.enabled = false;
        }
    }

    public override void Interact()
    {
        if (GameController.Instance.dialogueManager != null)
            dialogueManager = GameController.Instance.dialogueManager;

        base.Interact();

        dialogueManager.StartDialogue(Reply.Mulchant_Exit_Door);

        eventTripped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(eventTripped == false && other.CompareTag("Player"))
        {
            //Set off Dialogue here and cam event
            mulchantRenderer.enabled = true;
            mulchantCollider.enabled = true;
            Interact();
        }
    }
}
