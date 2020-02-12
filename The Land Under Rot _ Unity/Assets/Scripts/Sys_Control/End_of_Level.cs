using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class End_of_Level : MonoBehaviour
{
    public GameObject endLevelDisplay;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        endLevelDisplay.SetActive(false);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            endLevelDisplay.SetActive(true);
            if (gameController!= null && gameController.playerController != null)
            {
                gameController.playerController.enabled = false;
            }
        }
    }
}
