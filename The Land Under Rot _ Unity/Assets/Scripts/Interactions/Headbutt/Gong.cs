using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong : Interactable
{

    public override void Interact()
    {
        base.Interact();

        // Moved 'PlaySound' to the base Interactable
        // bc each obj will play a sound if they have one
        // regardless of the object. And if they don't have one 
        // assigned then they won't play a sound 
        GameController.Instance.tutorial_bus_Called = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }
}
