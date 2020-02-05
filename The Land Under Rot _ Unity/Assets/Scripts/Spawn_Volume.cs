using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Volume : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerSpawn;
    public Transform[] spawnPoints;
    public float[] spawnDistances;
    public int distanceIndex;
    public bool activeCorridor;

    public bool single_Spawn;
    public bool region_Spawn;
    public bool single_Kill_Volume;
    public bool region_Kill_Volume;

    private SpawnType spawnType;

    [SerializeField]
    private float shortestDist;

    public bool activeSpawnArea;

    public GameObject gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(single_Spawn || region_Spawn)
        {
            spawnType = SpawnType.RESPAWN;
        }
        else if(single_Kill_Volume || region_Kill_Volume)
        {
            spawnType = SpawnType.KILL;
        }

        if(region_Spawn)
        {
            spawnDistances = new float[spawnPoints.Length];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(region_Spawn)
        {
            if (activeSpawnArea == true)
            {
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    spawnDistances[i] = Vector3.Distance(spawnPoints[i].position, gamePlayer.transform.position);

                    if (shortestDist > spawnDistances[i])
                    {
                        distanceIndex = i;
                    }

                    shortestDist = spawnDistances[distanceIndex];
                    //playerSpawn = spawnPoints[distanceIndex];
                    spawnPoint = spawnPoints[distanceIndex];
                }
            }
        }
        else if(single_Spawn)
        {
            if (playerSpawn != spawnPoint)
            {
                activeCorridor = false;
            }
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            if(single_Spawn)
            {
                player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
                playerSpawn = player.GetComponent<PlayerController>().currentSpawn;
                activeCorridor = true;
            }
            else if(region_Spawn)
            {
                shortestDist = 99999.0f;
                activeSpawnArea = true;
            }
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if(region_Spawn)
        {
            if(activeSpawnArea)
            {
                gamePlayer = player.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(region_Spawn)
        {
            if (player.CompareTag("Player"))
            {
                activeSpawnArea = false;
            }
        }
    }
}
