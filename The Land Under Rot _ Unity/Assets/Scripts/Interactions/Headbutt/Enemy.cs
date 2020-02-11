using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    public int health = 1;

    public override void Interact()
    {
        base.Interact();

        health = health - 1;
    }

    private void Update()
    {
        if (health <= 0)
        {
            //DIE

            Destroy(gameObject); // TODO: Better enemy death/stun(?)
        }
    }
}
