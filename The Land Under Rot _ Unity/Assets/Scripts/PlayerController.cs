using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    #region Player Components
    public GameObject playerCamera;
    private Animator animator;
    private CamControl camControl;
    private Rigidbody rb;
    //Will eventually search spawner out by scene
    public Transform currentSpawn;
    #endregion

    [Header("Player Modifiers")]
    #region Modifiers
    public int health;
    public float speed = 12;
    public float rotateVel = 100;
    public float jumpForce;
    public float deathfadeDelay;
    #endregion

    [Header("Booleans")]
    #region Bools
    public bool isDead;
    public bool isGrounded;
    public bool isHeadBangin;
    public bool shouldInteract;
    #endregion

    [Header("Input Vectors")]
    #region Input
    public float inputDelay = 0.1f;
    public Vector2 controlInput;
    private float horizontalInput;
    private float verticalInput;
    private Quaternion targetRotation;
    #endregion

    //Hash ID
    private int MovementID = 0;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("Rigidbody missing on " + gameObject.name);
        else
            rb = GetComponent<Rigidbody>();

        if (animator != null)
            animator = GetComponent<Animator>();

        //camControl = playerCamera.GetComponent<CamControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Find Spawn Point
        //this.transform.position = currentSpawn.position;
        //this.transform.rotation = currentSpawn.rotation;

        //targetRotation = currentSpawn.rotation;

        //forwardVel = turnInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        if (Mathf.Abs(horizontalInput) < inputDelay)
            horizontalInput = 0;
        if (Mathf.Abs(verticalInput) < inputDelay)
            verticalInput = 0;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime;

        transform.position += movement;
        //rb.AddForce(movement);
    }

    public Quaternion GetTargetRotation()
    {
        return targetRotation;
    }

}
