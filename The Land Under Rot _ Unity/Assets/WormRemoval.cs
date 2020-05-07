using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormRemoval : MonoBehaviour
{
    Vector3 newStart = new Vector3(0, -450, 0);

    // Start is called before the first frame update
    void Start()
    {
       if(GameController.Instance.wormsInFruitfulGone)
        {
            transform.position = newStart;
        }
    }
}
