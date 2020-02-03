using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public float interactDelay = 1f;

    public virtual void Interact()
    {
        // THis method is meant to be overwritten
    }
}

