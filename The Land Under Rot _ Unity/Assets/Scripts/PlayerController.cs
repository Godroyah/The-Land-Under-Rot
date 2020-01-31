using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    #region Player Components
    public GameObject playerCamera;
    private Animator animator;
    public CamControl camControl;
    private Rigidbody rb;
    //Will eventually search spawner out by scene
    public Transform currentSpawn;
    #endregion

    [Header("Player Modifiers")]
    #region Modifiers
    public int health = 3;
    [Range(1, 20)]
    public float speed = 12;
    [Range(1, 20)]
    public float jumpStrength = 10f;
    public float rotateVel = 100;
    public float minRotationDelay = 15f;
    public float rotationSpeed = 0.1f;
    public float deathfadeDelay;
    [Range(0.001f,5)]
    public float fallMultiplierFloat = 2f; // TODO: Look at this for GLIDING later
    #endregion

    [Header("Booleans")]
    #region Bools
    public bool isDead;
    public bool isGrounded;
    public bool isHeadBangin;
    public bool shouldInteract;
    public bool shouldJump = false;
    #endregion

    [Header("Input")]
    #region Input
    public float inputDelay = 0.1f;
    [Range(0.001f, 1), Tooltip("This determines how responsive the obj is to movement.")]
    public float rotationTargetDist = 0.1f;
    [Range(0, 1), Tooltip("This will be the cutoff distance for when the obj will rotate. This will also be capped by the rotationTargetDist.")]
    public float rotationCutoff = 0f;
    public Vector2 controlInput;
    private float horizontalInput;
    private float verticalInput;
    private Quaternion targetRotation;
    private Transform rotationTarget;
    #endregion

    //Hash ID
    private int MovementID = 0;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("Rigidbody missing on " + gameObject.name);
        else
            rb = GetComponent<Rigidbody>();

        if (camControl == null)
            Debug.LogWarning("Camera Controller Missing!!");

        if (animator != null)
            animator = GetComponent<Animator>();

        rotationTarget = new GameObject().transform;
        rotationTarget.transform.parent = gameObject.transform;
        rotationTarget.name = "RotationTarget$$";

        camControl.rotationTarget = rotationTarget;

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


        // TODO: Limit this to happen if outside of specific range
        //       Maybe if no input don't move rotationTarget?
        if (Vector3.Distance(transform.position, rotationTarget.position) > rotationCutoff)
        {
            Rotate();
        }

        if (shouldJump)
        {
            rb.AddForce(rotationTarget.position + Vector3.up * jumpStrength * 50);
            shouldJump = false;
        }

        //faster falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        shouldJump = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        if (Mathf.Abs(horizontalInput) < inputDelay)
            horizontalInput = 0;
        if (Mathf.Abs(verticalInput) < inputDelay)
            verticalInput = 0;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 tempDir = rotationTarget.TransformDirection(movement * rotationTargetDist); // TODO: Hardcoded Detection Radius
        rotationTarget.position = tempDir + transform.position;

        movement = rotationTarget.TransformDirection(movement);
        movement *= speed * Time.deltaTime;

        transform.position += movement;
        //rb.AddForce(movement);
    }

    private void Rotate()
    {
        //find the vector pointing from our position to the target
        Vector3 _direction = (rotationTarget.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed); // TODO: Smoother Transitions
                                                                                                                  // ^^ For smoother transitions the speed should increase if the distance is shorter
    }

}
