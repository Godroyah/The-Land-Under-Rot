using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn_Corridor : MonoBehaviour
{
    public Transform spawnPoint;
    public PlayerController playerController;

    private void Start()
    {
        if(playerController == null)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
        }
    }
}
