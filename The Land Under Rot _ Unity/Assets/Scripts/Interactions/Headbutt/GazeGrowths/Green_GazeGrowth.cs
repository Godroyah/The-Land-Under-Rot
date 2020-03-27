﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_GazeGrowth : GazeGrowth
{
    public bool isOpen = false;

    public GameObject openColliders;
    public GameObject closedColliders;

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

        // TODO: Anim Triggers
        /*
        animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);
        */

        isOpen = !isOpen;

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
