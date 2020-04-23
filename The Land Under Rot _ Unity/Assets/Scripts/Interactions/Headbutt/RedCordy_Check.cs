using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCordy_Check : MonoBehaviour
{

    public RedBlue_GazeGrowth redGazeGrowth;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            redGazeGrowth.isCordyReached = true;
        }
    }
}
