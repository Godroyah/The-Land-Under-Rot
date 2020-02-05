using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSpawn : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform[] spawnPoints;
    public float[] spawnDistances;
    public int distanceIndex;

    
    [SerializeField]
    private float shortestDist;

    public bool activeSpawnArea;

    public GameObject gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        spawnDistances = new float[spawnPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(activeSpawnArea == true)
        {
           for (int i = 0; i < spawnPoints.Length; i++)
           {
                spawnDistances[i] = Vector3.Distance(spawnPoints[i].position, gamePlayer.transform.position);
                
                if(shortestDist > spawnDistances[i])
                {
                    distanceIndex = i;
                }

                shortestDist = spawnDistances[distanceIndex];
                playerSpawn = spawnPoints[distanceIndex];
                gamePlayer.GetComponent<PlayerController>().currentSpawn = playerSpawn;
           }
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            shortestDist = 99999.0f;
            activeSpawnArea = true;
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if(activeSpawnArea)
        {
            gamePlayer = player.gameObject;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            activeSpawnArea = false;
        }
    }
}
