using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Branch_Type { NONE, STAR, WILLOW}

public class Contact_CamEvent : Interactable
{
    public Branch_Type branch;

    BoxCollider thisCollider;

    GameController gameController;

    private void Start()
    {
        thisCollider = GetComponent<BoxCollider>();
        gameController = GameController.Instance;

        if(branch == Branch_Type.STAR && gameController.starTreeAwake)
        {
            thisCollider.enabled = true;
        }
        else if(branch == Branch_Type.WILLOW && gameController.willowTreeAwake)
        {
            thisCollider.enabled = true;
        }
        else
        {
            thisCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
