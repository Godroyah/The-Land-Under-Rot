using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected PlayerController playerController;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("This interactable does not have a Rigidbody!");

        #region PlayerController Search
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        if (temp != null)
        {
            playerController = temp.GetComponent<PlayerController>();
            if (playerController == null)
                Debug.LogWarning("Player does not have the 'PlayerController' script!");
        }
        else
        {
            Debug.LogWarning("Could not find PlayerController.");
        }
        #endregion
    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log(gameObject.name + " has been interacted with.");
    }
}

