using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBlue : MonoBehaviour
{
    public Transform target;
    public float speed = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        direction.Normalize();
        rb.AddForce(direction * speed * Time.deltaTime);
    }
}
