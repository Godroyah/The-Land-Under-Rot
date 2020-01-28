using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Player Components")]
    //Player Components
    public GameObject playerCamera;
    private Animator animator;
    private CamControl camControl;
    private Rigidbody rb;
    //Will eventually search spawner out by scene
    public Transform currentSpawn;


    public int health;
    public bool grounded;
    public float walkSpeed;
    public float jumpSpeed;
    public bool headBangin;
    public bool interact;
    
    public float fadeDelay;
    public bool dead;
    public Vector2 controlInput;


    //Hash ID
    private int MovementID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //
        this.transform.position = currentSpawn.position;
        this.transform.rotation = currentSpawn.rotation;

        rb = GetComponent<Rigidbody>();

        if (animator != null)
            animator = GetComponent<Animator>();

        camControl = playerCamera.GetComponent<CamControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
