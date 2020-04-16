using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_GazeGrowth : GazeGrowth
{
    public bool isOpen = false;

    public GameObject openColliders;
    public GameObject closedColliders;

    public Animator flowerAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        base.Interact();

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

        /* Original Test
        if (isOpen)
        {
            openColliders.SetActive(isOpen);
            closedColliders.SetActive(!isOpen);
        }
        */
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
