using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Volume : MonoBehaviour
{
    public GameObject spawnPoint;

    public bool startSpawn;
    public bool respawnVolume;
    public bool killVolume;

    // TODO: Bool options to mark as Start Spawn for level, Local Respawn, or Kill Volume
    // TODO: Make provisions for gathering previous scene info from previous level to determine Start Spawn
    // TODO: Kill Volume health/death interaction and player death/teleport


    // Start is called before the first frame update
    void Start()
    {
        if(startSpawn)
        {
            gameObject.tag = "Start_Spawn";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider creature)
    {
        
    }

    void OnTriggerExit(Collider creature)
    {
        
    }

    void StartSpawn()
    {

    }
}
