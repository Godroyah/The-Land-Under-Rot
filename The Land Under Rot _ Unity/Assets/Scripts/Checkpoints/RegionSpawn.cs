using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSpawn : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform[] spawnPoints;
    //private List<float> spawnDistance;
    
    private float shortestDist;
    private float currentDist;

    public bool activeSpawnArea;

    public PlayerController playerController;
    public GameObject gamePlayer;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.areaSpawnCalc == true)
        {
            for(int i = 0; i <= spawnPoints.Length; i++)
            {
                currentDist = Vector3.Distance(gamePlayer.transform.position, spawnPoints[i].position);
                if(currentDist < shortestDist)
                {
                    shortestDist = currentDist;
                    playerSpawn = spawnPoints[i].transform;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        activeSpawnArea = true;
        gameController.areaSpawnCalc = true;
        gamePlayer = player.gameObject;
        gamePlayer.GetComponent<PlayerController>().currentSpawn = playerSpawn; 
    }

    private void OnTriggerExit(Collider player)
    {
        activeSpawnArea = false;
    }


}
