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

    [Header("Player Modifiers")]
    public int health;
    public float walkSpeed;
    public float jumpForce;
    public float deathfadeDelay;

    [Header("Booleans")]
    public bool dead;
    public bool grounded;
    public bool headBangin;
    public bool interact;

    [Header("Input Vectors")]
    public Vector2 controlInput;
    private float horizontalVelocity;
    private float verticalVelocity;

    //Hash ID
    private int MovementID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Will eventually seek out spawn point by scene
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

    void MoveInput()
    {
        //horizontalVelocity = Input.GetAxisRaw("Horizontal");
        //verticalVelocity = Input.GetAxisRaw("Vertical");


    }

    void MoveCalc()
    {

    }

    void JumpInput()
    {

    }

    void JumpCalc()
    {

    }


}
