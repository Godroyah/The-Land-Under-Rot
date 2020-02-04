using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSpawn : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform[] spawnPoints;
    //public int listLength;
    public bool calculating;


    public float[] spawnDistances;
    public int distanceIndex;

    //public List<float> spawnDistance;
    //private List<float> spawnDistance;
    
    [SerializeField]
    private float shortestDist;
    //[SerializeField]
    //private float[] currentDists;

    public bool activeSpawnArea;

    //public PlayerController playerController;
    public GameObject gamePlayer;
    //public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        calculating = false;
        //gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //spawnListSize = spawnPoints.Length;
        //spawnDistance = new List<float>(spawnPoints.Length);
        spawnDistances = new float[spawnPoints.Length];
        //currentDists = new float[spawnPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(activeSpawnArea == true)
        {
            //foreach (Transform i in spawnPoints)
            //{
                
            //    //Debug.Log("Calculating?");
            //    spawnDistance.IndexOf(i) = Vector3.Distance(i.position, gamePlayer.transform.position);
                
               
            //    if (currentDist < shortestDist)
            //    {
            //        playerSpawn = i;
            //        shortestDist = currentDist;
            //    }
            //}
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                //Debug.Log("Calculating?");
                spawnDistances[i] = Vector3.Distance(spawnPoints[i].position, gamePlayer.transform.position);
                //shortestDist = spawnDistances[i];

                if(shortestDist > spawnDistances[i])
                {
                    distanceIndex = i;
                    //shortestDist = spawnDistances[i];
                    //playerSpawn = spawnPoints[i];
                }

                shortestDist = spawnDistances[distanceIndex];
                playerSpawn = spawnPoints[distanceIndex];

                



                //if(currentDist > spawnDistances[i])
                //{
                //    calculating = false;
                //    currentDist = spawnDistances[i];
                //}
                //else
                //{
                //    calculating = true;
                //    shortestDist = currentDist;
                //    playerSpawn = spawnPoints[i];
                //}

                //if (spawnDistances[i] < shortestDist)
                //{
                //    playerSpawn = spawnPoints[i].transform;
                //    shortestDist = spawnDistances[i];
                //}
            }

            //for(int j = 0; j < spawnPoints.Length; j++)
            //{
            //    if(spawnDistances[j] < shortestDist)
            //    {
            //        shortestDist = spawnDistances[j];
            //    }
                
            //}

        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            shortestDist = 99999.0f;
            //currentDist = 99999.0f;
            activeSpawnArea = true;
        }
       
        //foreach (Transform i in spawnPoints)
        //{
        //    spawnDistance.Add(shortestDist);
        //}
        //listLength = spawnDistances.Count;
    }

    private void OnTriggerStay(Collider player)
    {
        if(activeSpawnArea)
        {
            //gameController.areaSpawnCalc = true;
            gamePlayer = player.gameObject;
            //gamePlayer.GetComponent<PlayerController>().currentSpawn = playerSpawn;
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
