﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType { NONE, ACORN, MULCH, HEALTH, HARMFUL}

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
        if(pickUpType == PickUpType.ACORN || pickUpType == PickUpType.HEALTH || pickUpType == PickUpType.HARMFUL)
        {
            transform.Rotate(0, 3, 0, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pick me up!");
            PlayerController playerController = other.GetComponent<PlayerController>();

            switch (pickUpType)
            {
                case PickUpType.NONE:
                    Debug.LogWarning("PickUpType not set!");
                    break;
                case PickUpType.ACORN:
                    //Count ACORN;
                    //move to Player; have GameController call for it
                    playerController.acorns += 1;
                    Destroy(gameObject);
                    break;
                case PickUpType.MULCH:
                    //Count MULCH
                    playerController.mulch += 1;
                    Destroy(gameObject);
                    break;
                case PickUpType.HEALTH:
                    //Add HEALTH
                    if (playerController.health < 3)
                    {
                        playerController.health += 1;
                    }
                    Destroy(gameObject);
                    break;
                case PickUpType.HARMFUL:
                    //Take HEALTH
                    if (playerController.health > 0)
                    {
                        playerController.health -= 1;
                    }
                    Destroy(gameObject);
                    break;
                default:
                    Debug.LogWarning("PickUpType Error.");
                    break;
            }

        }
    }
}
