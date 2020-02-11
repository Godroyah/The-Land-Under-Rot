using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Interactable
{
    public override void Interact()
    {
        base.Interact();

        // TODO: Switch from Destroy to Particle/Anim
        Destroy(gameObject);
    }
}
