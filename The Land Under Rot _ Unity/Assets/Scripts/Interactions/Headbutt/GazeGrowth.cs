using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeGrowth : Interactable
{
    public GazeGrowthType growthType;

    public GameObject[] cordyseps;

    [Range(0f, 25f), Tooltip("If set to 0, the cordyseps will never return.")]
    public float returnIn = 0f;

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

        switch (growthType)
        {
            // TODO: Needs proper movement/animation
            case GazeGrowthType.White:
                StartCoroutine(Fungi(0));
                break;
            case GazeGrowthType.Red:
                StartCoroutine(Fungi(returnIn));
                break;
            default:
                break;
        }
    }

    IEnumerator Fungi(float waitTime)
    {
        foreach (GameObject fungi in cordyseps)
        {
            transform.position = transform.position - (Vector3.down * 20f);
        }

        if (waitTime != 0)
        {
            yield return new WaitForSeconds(waitTime);

            foreach (GameObject fungi in cordyseps)
            {
                transform.position = transform.position + (Vector3.up * 20f);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
            //gameController.playerController.headbuttables.Add(this);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Headbutt"))
    //    {
    //        gameController.playerController.headbuttables.Remove(this);
    //    }
    //}
}

public enum GazeGrowthType
{
    White, Red
}
