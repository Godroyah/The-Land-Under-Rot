using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType { NONE, START, RESPAWN, KILL}

public class Spawn_Volume : MonoBehaviour
{
    public SpawnType spawnType; // This gives a drop down menu of the spawn types

    public Transform spawnPoint;
    private Transform playerSpawn;
    public bool activeCorridor;
    public PlayerController playerController;

    //public GameObject gamePlayer;
    //public bool activeSpawnArea;

    //public Transform[] spawnPoints; // Couldn't this simply be multiple spawn volumes?
    //public float[] spawnDistances;
    //public int distanceIndex;

    /*
    public bool single_Spawn;
    public bool region_Spawn;
    public bool single_Kill_Volume;
    public bool region_Kill_Volume;
    */

    //[SerializeField]
    //private float shortestDist;

    //Start is called before the first frame update
    void Start()
    {
        //if(spawnType == SpawnType.REGION_RESPAWN)
        //{
        //    spawnDistances = new float[spawnPoints.Length];
        //}
        if(spawnType == SpawnType.START)
        {
            activeCorridor = true;
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.currentSpawn = spawnPoint;
            playerSpawn = playerController.currentSpawn;
            
            //-------------------------------------------------------
            //----This Start for Demo only---------------------------

        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnType == SpawnType.START || spawnType == SpawnType.RESPAWN) && activeCorridor && playerSpawn != spawnPoint)
        {
            activeCorridor = false;
            playerSpawn = spawnPoint;
        }
        //if (spawnType == SpawnType.RESPAWN)
        //{
        //    if (activeCorridor && playerSpawn != spawnPoint)
        //    {
        //        activeCorridor = false;
        //    }
        //}
        //if(spawnType == SpawnType.START)
        //{
        //    if(playerSpawn != null)
        //    {
        //       if(activeCorridor && playerSpawn != spawnPoint)
        //        {
        //            activeCorridor = false;
        //        }
        //    }
        //}
        //else if(spawnType == SpawnType.REGION_RESPAWN)
        //{
        //    if (activeSpawnArea == true)
        //    {
        //        for (int i = 0; i < spawnPoints.Length; i++)
        //        {
        //            spawnDistances[i] = Vector3.Distance(spawnPoints[i].position, gamePlayer.transform.position);

        //            if (shortestDist > spawnDistances[i])
        //            {
        //                distanceIndex = i;
        //            }

        //            shortestDist = spawnDistances[distanceIndex];
        //            //playerSpawn = spawnPoints[distanceIndex];
        //            spawnPoint = spawnPoints[distanceIndex];
        //        }
        //    }
        //}
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            playerController = player.GetComponent<PlayerController>();
            if (spawnType == SpawnType.RESPAWN)
            {
                //player.GetComponent<PlayerController>().currentSpawn = spawnPoint;
                playerSpawn = playerController.currentSpawn;
                playerController.currentSpawn = spawnPoint.transform;
                activeCorridor = true;
            }
            else if(spawnType == SpawnType.KILL)
            {
                playerController.Respawn();
            }
            //else if(spawnType == SpawnType.REGION_RESPAWN)
            //{
            //    shortestDist = 99999.0f;
            //    activeSpawnArea = true;
            //}
        }
    }

    //private void OnTriggerStay(Collider player)
    //{
    //    if(spawnType == SpawnType.REGION_RESPAWN)
    //    {
    //        if(activeSpawnArea)
    //        {
    //            gamePlayer = player.gameObject;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider player)
    //{
    //    if(spawnType == SpawnType.REGION_RESPAWN)
    //    {
    //        if (player.CompareTag("Player"))
    //        {
    //            activeSpawnArea = false;
    //        }
    //    }
    //}
}
