using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Interactable
{

    GameController gameController;

    private void Start()
    {
        #region GameController Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();

            if (gameController == null)
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
            Debug.LogWarning("Could not find GameController.");

        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        // TODO: Switch from Destroy to Particle/Anim
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            gameController.playerController.headbuttables.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            gameController.playerController.headbuttables.Remove(this);
        }
    }
}
