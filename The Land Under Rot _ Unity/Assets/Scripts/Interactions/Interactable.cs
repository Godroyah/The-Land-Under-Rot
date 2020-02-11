﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public float interactDelay = 1f;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("This interactable does not have a Rigidbody!");
    }

    public virtual void Interact()
    {
        // THis method is meant to be overwritten
    }
}

