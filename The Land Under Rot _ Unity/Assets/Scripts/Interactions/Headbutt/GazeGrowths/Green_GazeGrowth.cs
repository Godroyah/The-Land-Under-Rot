using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_GazeGrowth : GazeGrowth
{
    [Tooltip("Time to wait in seconds")]
    public float changeDelay;

    public bool isOpen = false;

    public GameObject openColliders;
    public GameObject closedColliders;

    public Animator flowerAnimator;

    bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        thisDetector = GetComponent<SphereCollider>();

        if (isOpen)
        {
            closedColliders.SetActive(false);
            flowerAnimator.SetTrigger(FlowerAnims.Platform_Open.ToString());
        }
        else
        {
            openColliders.SetActive(false);
        }
    }

    public override void Interact()
    {
        if(!isTriggered)
        {
            isTriggered = true;
            thisDetector.enabled = false;
            base.Interact();

            animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
            animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);


            StartCoroutine(OpenFlower());
        }
    }

    IEnumerator OpenFlower()
    {
        yield return new WaitForSeconds(changeDelay);

        isOpen = !isOpen;

        if (isOpen)
        {
            flowerAnimator.SetTrigger(FlowerAnims.Platform_Open.ToString());
        }
        else
        {
            flowerAnimator.SetTrigger(FlowerAnims.Platform_Close.ToString());
        }

        // Alternates the colliders so that one is always on
        openColliders.SetActive(isOpen);
        closedColliders.SetActive(!isOpen);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }

}

public enum FlowerAnims
{
    Platform_Open, Platform_Close
}
