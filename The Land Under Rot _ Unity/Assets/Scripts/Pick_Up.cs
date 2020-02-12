using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType { NONE, ACORN, MULCH, HEALTH}

public class Pick_Up : MonoBehaviour
{
    public PickUpType pickUpType; //Drop down menu for the types of pickups (Acorn, Mulch, etc.)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpType == PickUpType.ACORN || pickUpType == PickUpType.HEALTH || CompareTag("Harmful"))
        {
            transform.Rotate(0, 3, 0, Space.World);
        }
    }
}
