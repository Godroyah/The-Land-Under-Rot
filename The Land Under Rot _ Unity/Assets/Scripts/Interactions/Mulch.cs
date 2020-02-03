using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulch : Interactable
{
    public GameObject[] objsToSpawn;

    public float spawnJumpHeight = 1f;

    public override void Interact()
    {
        base.Interact();

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
        }
    }
}
