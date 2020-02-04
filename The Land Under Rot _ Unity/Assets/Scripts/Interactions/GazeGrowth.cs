using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeGrowth : Interactable
{
    public GazeGrowthType growthType;

    public GameObject[] cordyseps;

    [Range(0f, 25f), Tooltip("If set to 0, the cordyseps will never return.")]
    public float returnIn = 0f;

    public override void Interact()
    {
        base.Interact();

        switch (growthType)
        {
            // TODO: Needs proper movement/animation
            case GazeGrowthType.White:

                break;
            case GazeGrowthType.Red:
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
}

public enum GazeGrowthType
{
    White, Red
}
