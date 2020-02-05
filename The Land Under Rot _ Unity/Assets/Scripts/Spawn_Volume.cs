using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType { NONE, START, SINGLE_RESPAWN, REGION_RESPAWN, SINGLE_KILL, REGION_KILL }

public class Spawn_Volume : MonoBehaviour
{
    public SpawnType spawnType; // This gives a drop down menu of the spawn types

    public Transform spawnPoint;
    public Transform playerSpawn;
    public Transform[] spawnPoints; // Couldn't this simply be multiple spawn volumes?
    public float[] spawnDistances;
    public int distanceIndex;
    public bool activeCorridor;

    /*
    public bool single_Spawn;
    public bool region_Spawn;
    public bool single_Kill_Volume;
    public bool region_Kill_Volume;
    */

    

    [SerializeField]
    private float shortestDist;

    public bool activeSpawnArea;

    public GameObject gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnType == SpawnType.REGION_RESPAWN)
        {
            spawnDistances = new float[spawnPoints.Length];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnType == SpawnType.REGION_RESPAWN)
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
        else if(spawnType == SpawnType.SINGLE_RESPAWN)
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
            if(spawnType == SpawnType.SINGLE_RESPAWN)
            {
                player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
                playerSpawn = player.GetComponent<PlayerController>().currentSpawn;
                activeCorridor = true;
            }
            else if(spawnType == SpawnType.REGION_RESPAWN)
            {
                shortestDist = 99999.0f;
                activeSpawnArea = true;
            }
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if(spawnType == SpawnType.REGION_RESPAWN)
        {
            if(activeSpawnArea)
            {
                gamePlayer = player.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(spawnType == SpawnType.REGION_RESPAWN)
        {
            if (player.CompareTag("Player"))
            {
                activeSpawnArea = false;
            }
        }
    }
}
