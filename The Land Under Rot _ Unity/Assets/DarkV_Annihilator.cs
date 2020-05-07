using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkV_Annihilator : MonoBehaviour
{
    public GameObject[] Darkvolumes;

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.Instance.underStumpLightsOn == true)
        {
            foreach (GameObject darkVolume in Darkvolumes)
            {
                darkVolume.SetActive(false);
            }
        }
    }
}
