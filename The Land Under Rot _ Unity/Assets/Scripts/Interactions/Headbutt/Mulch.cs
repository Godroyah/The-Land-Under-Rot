using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulch : Interactable
{
    public GameObject[] objsToSpawn;

    public GameObject mulchContainer;

    public Collider mulchCollider;

    public float spawnJumpHeight = 1f;

    private int numberSpawned;

    GameController gameController;

    private void Start()
    {
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

        //foreach (GameObject obj in objsToSpawn)
        //{
        //    GameObject temp = Instantiate(obj, transform);
        //    temp.transform.position = transform.position;

        //    Rigidbody rb;
        //    rb = temp.GetComponent<Rigidbody>();

        //    if (rb == null) // TODO: Mulch Spawn Rigidbodies
        //        rb = temp.AddComponent<Rigidbody>();

        //    temp.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        //    rb.AddForce((Vector3.left / 3f) + Vector3.up * spawnJumpHeight);

        //    numberSpawned += 1;
        //}

        //if (numberSpawned >= objsToSpawn.Length)
        //    Destroy(mulchContainer);



        foreach (GameObject obj in objsToSpawn)
        {
            GameObject temp = Instantiate(obj, transform);
            temp.transform.position = transform.position;

            Rigidbody rb;
            rb = temp.GetComponent<Rigidbody>();

            if (rb == null) // TODO: Mulch Spawn Rigidbodies
                rb = temp.AddComponent<Rigidbody>();

            temp.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

            rb.AddForce((Vector3.left / 3f) + Vector3.up * spawnJumpHeight);

            numberSpawned += 1;
        }

        if (numberSpawned >= objsToSpawn.Length)
        {
            transform.DetachChildren();
            Destroy(mulchContainer);
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
