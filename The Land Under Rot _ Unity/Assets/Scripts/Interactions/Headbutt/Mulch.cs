using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulch : Interactable
{
    public GameObject[] objsToSpawn;

    public Transform[] spawnPoints;

    public GameObject mulchContainer;

    public Collider mulchCollider;

    public float spawnJumpHeight;

    private int numberSpawned;

    private float itemDirectionX;
    private float itemDirectionZ;

    private float directionControllerX;
    private float directionControllerZ;

    //private Bounds spawnBounds;

    //[SerializeField]
    //private Vector3 randomSpawn;

    //private Vector3 spawnStart;

    GameController gameController;

    private void Start()
    {
        
        //randomSpawn = Vector3.zero;
        //spawnBounds = mulchCollider.bounds;

        //spawnStart = mulchCollider.transform.position;

        //randomSpawn = spawnStart;

        #region GameController Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();

            if (gameController == null)
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
            Debug.LogWarning("Could not find GameController.");

        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        mulchCollider.enabled = false;
        if(objsToSpawn.Length == spawnPoints.Length)
        {
            foreach (GameObject obj in objsToSpawn)
            {
                GameObject temp;
                temp = Instantiate(obj, transform) as GameObject;

                temp.transform.position = spawnPoints[numberSpawned].position;

                //Randomize Spawning - Revisit
                #region
                //if(randomSpawn == spawnStart)
                // {
                //     temp.transform.position = randomSpawn;
                // }
                //else
                // {
                //     randomSpawn = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), Random.Range(spawnBounds.min.y, spawnBounds.max.y), Random.Range(spawnBounds.min.z, spawnBounds.max.z));
                // }

                //if(randomSpawn == Vector3.zero)
                //{
                //    randomSpawn = new Vector3(Random.Range )
                //}

                //temp.transform.position = new Vector3(Random.Range())

                //transform.position;
                #endregion  //Randomize spawning

                Rigidbody rb;
                rb = temp.GetComponentInChildren<Rigidbody>();

                if (rb == null) // TODO: Mulch Spawn Rigidbodies
                    rb = temp.AddComponent<Rigidbody>();

                directionControllerX = Random.Range(0, 1);
                directionControllerZ = Random.Range(0, 1);

                if (directionControllerX > 0.5)
                {
                    itemDirectionX = 100f;
                }
                else
                {
                    itemDirectionX = -100f;
                }

                if (directionControllerZ > 0.5)
                {
                    itemDirectionZ = 100f;
                }
                else
                {
                    itemDirectionZ = -100f;
                }

                temp.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

                rb.AddForce((new Vector3(Random.Range(-1, 1) * itemDirectionX, 0, Random.Range(-1, 1) * itemDirectionZ) + mulchContainer.transform.up * spawnJumpHeight));

                numberSpawned += 1;
            }

            if (numberSpawned >= objsToSpawn.Length)
            {
                transform.DetachChildren();
                Destroy(mulchContainer);
            }
        }
        else
        {
            Debug.LogWarning("Objs To Spawn and Spawn Points arrays on Mulch do not have the same number of elements!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Headbutt"))
    //    {
    //        gameController.playerController.headbuttables.Remove(this);
    //    }
    //}
}
