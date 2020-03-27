using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_GazeGrowth : GazeGrowth
{
    public bool isOpen = false;

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

        animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);

        isOpen = !isOpen;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }
}
