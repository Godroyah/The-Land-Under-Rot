using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerSpawn;
    public GameController gameController;
    //public Transform previousSpawn;
    //private PlayerController playerController;

    public bool activeCorridor;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if(playerSpawn == null)
        {
            return;
        }
    }

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
            //gameController.areaSpawnCalc = false;
            //previousSpawn = player.GetComponent<PlayerController>().currentSpawn;
            player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
            playerSpawn = player.GetComponent<PlayerController>().currentSpawn;
            activeCorridor = true;
        }
    }
}
