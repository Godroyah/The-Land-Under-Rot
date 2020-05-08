using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Stump_Turnoff : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if(GameController.Instance.underStumpLightsOn)
        {
            this.gameObject.SetActive(false);
        }
    }

}
