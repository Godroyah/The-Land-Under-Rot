using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Volume_Single : MonoBehaviour
{

    public bool instantKill;
    public bool healthDrain;
    public GameObject gamePlayer;
    public PlayerController playerController;

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            playerController = player.GetComponent<PlayerController>();
            //playerController.isDead = true;
        }
    }
}
