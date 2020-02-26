using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerSpawn;

    public bool activeCorridor;

    private void Update()
    {
        if(playerSpawn != spawnPoint)
        {
            activeCorridor = false;
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
            playerSpawn = player.GetComponent<PlayerController>().currentSpawn;
            activeCorridor = true;
        }
    }
}
