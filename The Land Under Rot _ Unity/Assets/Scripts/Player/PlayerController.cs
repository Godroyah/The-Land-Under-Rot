using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    #region Player Components
    //public GameObject playerCamera;
    private Animator animator;
    public CamControl camControl;
    private Rigidbody rb;
    //Will eventually search spawner out by scene
    public Transform currentSpawn;
    //public Transform playerHolder;
    #endregion

    [Header("Player Modifiers")]
    #region Modifiers
    //public int health = 3;

    [Range(1, 20)]
    public float moveSpeed = 12f;

    [Range(1, 20)]
    public float jumpStrength = 10f;

    //[Range(0,200), Tooltip("Controls how fast the obj rotates when moving.")]
    //public float rotateVel = 100;
    //public float minRotationDelay = 15f;

    [Range(0, 200), Tooltip("Controls how fast the obj rotates when moving.")]
    public float rotationSpeed = 0.1f;

    //public float deathfadeDelay;

    [Range(0.001f, 5), Tooltip("Controls how fast the Player will fall.")]
    public float fallMultiplierFloat = 2f; // TODO: Look at this for GLIDING later


    //public bool interactionRange_Visibililty = false;
    [Range(0f, 5f), Tooltip("Determines the Radius for Interactions.")]
    public float interactionRange = 1f;

    [Tooltip("This field helps specify that the SphereCollider on the Player is used for Interaction Detection.")]
    public SphereCollider interactionDetector; // TODO: Will this sphere collider cause issues?
    #endregion

    //[Header("Camera Override")]


    [Header("Booleans")]
    #region Bools
    //public bool isDead;
    //private bool isDead;
    private bool isGrounded;
    private bool isHeadBangin;
    private bool shouldInteract = false;
    private bool shouldJump = false;
    #endregion


    [Header("Input")]
    #region Input
    [Range(0, 1), Tooltip("Cutoff for the value when the input is accepted.")]
    public float inputDelay = 0.1f;
    [Range(0, 1), Tooltip("Delay until the next time 'Interact' is available.")]
    public float interactDelay = 0.1f;
    private Coroutine interactingCoroutine;
    [Range(0.001f, 1), Tooltip("Determines how responsive the obj is to movement.")]
    public float rotationTargetDist = 0.1f;
    [Range(0, 1), Tooltip("Cutoff distance for when the obj will rotate. This will also be capped by the rotationTargetDist.")]
    public float rotationCutoff = 0f;
    [HideInInspector]
    public Vector2 controlInput;
    private float horizontalInput;
    private float verticalInput;
    private Quaternion targetRotation;
    private Transform rotationTarget;
    #endregion

    private GameController gameController;
    private Interactable currentTarget = null;

    //Hash ID
    private int MovementID = 0;

    private void Awake()
    {
        // TODO: Make this not rely on game controller
        // Player cannot work without it as of rn
        //gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("Rigidbody missing on " + gameObject.name);
        else
            rb = GetComponent<Rigidbody>();

        if (camControl == null)
            Debug.LogWarning("Camera Controller Missing!!");

        if (animator != null)
            animator = GetComponent<Animator>();

        if (interactionDetector == null)
        {
            interactionDetector = GetComponent<SphereCollider>();
            if (interactionDetector == null)
            {
                Debug.LogWarning("Interaction Detector SphereCollider Missing Reference!");
                Debug.Log("Adding a temp collider to " + gameObject.name + ". This requires more resources than starting with the component!");
                gameObject.AddComponent<SphereCollider>().radius = interactionRange;
            }
            
        }

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

        if (currentSpawn == null)
        {
            currentSpawn = transform;
        }

        this.transform.position = currentSpawn.position;
        this.transform.rotation = currentSpawn.rotation;


        //targetRotation = currentSpawn.rotation;

        //forwardVel = turnInput = 0;
    }

    // Update is called once per frame
    void Update()
    {


        GetInput();

        #region Interactable Check

        //Redefine above
        List<Interactable> interactables = new List<Interactable>();

        if (interactables.Count > 0) // If there are interactables in range...
        {
            Interactable closestTarget = null;
            float minimalDistance = float.MaxValue;
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            for (int targetIndex = 0; targetIndex < interactables.Count; targetIndex++)
            {
                Interactable target = interactables[targetIndex];
                if (target == null) // Check to see if the interactable still exists
                {
                    interactables.RemoveAt(targetIndex);
                    return;
                }
                Vector3 targetScreenPoint = Camera.main.WorldToScreenPoint(target.transform.position);

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(targetScreenPoint); // TODO: Make sure this ray goes forward

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    if (hit.collider.gameObject != interactables[targetIndex].gameObject) // If the targeted gameObject wasn't the object hit
                        continue; // If it doesn't hit, it doesn't see it

                    float distance = Vector2.Distance(targetScreenPoint, screenCenter);

                    if (distance < minimalDistance) // If the targeted gameObject is closer to the center of the screen than the previously found
                    {
                        minimalDistance = distance;
                        closestTarget = target;
                    }
                }

                if (closestTarget != null)
                {
                    if (currentTarget != null)
                    {
                        //ToggleHighlight(tempFocus, false);
                    }
                    currentTarget = closestTarget;
                }
            }

            if (currentTarget != null)
            {
                if (interactables.Contains(currentTarget)) // TODO: Another check to see if it still exists/in range
                {
                    ToggleHighlight(currentTarget, true);
                }
                else
                {
                    ToggleHighlight(currentTarget, false);
                    currentTarget = null;
                }
            }
        }

        #endregion


        // TODO: Limit this to happen if outside of specific range
        //       Maybe if no input don't move rotationTarget?
        if (Vector3.Distance(transform.position, rotationTarget.position) > rotationCutoff)
        {
            Rotate();
        }

        if (shouldJump)
        {
            rb.AddForce(((rotationTarget.localPosition + Vector3.up).normalized * jumpStrength * 50));
            shouldJump = false;
        }

        //faster falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
        }

        if (shouldInteract && interactingCoroutine == null)
        {
            StartCoroutine(Interacting());
        }
        /* 
        if (gameController.isDead)
        {
            Respawn();
            gameController.Reset();
        }
        */
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
        shouldInteract = Input.GetButtonDown("Interact");
    }

    private void ToggleHighlight(Interactable focus, bool state)
    {
        // TODO: Implement a type of Highlighting
    }

    IEnumerator Interacting()
    {
        if (currentTarget != null)
        {
            currentTarget.Interact(); // TODO: Find interactable target
        }

        yield return new WaitForSeconds(interactDelay);
        interactingCoroutine = null;
    }

    private void Move()
    {
        // Don't start accepting input until it peaks the inputDelay
        if (Mathf.Abs(horizontalInput) < inputDelay)
            horizontalInput = 0;
        if (Mathf.Abs(verticalInput) < inputDelay)
            verticalInput = 0;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 tempDir = rotationTarget.TransformDirection(movement * rotationTargetDist); // TODO: Hardcoded Detection Radius
        rotationTarget.position = tempDir + transform.position;

        movement = rotationTarget.TransformDirection(movement);
        movement *= moveSpeed * Time.deltaTime;

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

    private void Respawn()
    {
        transform.position = currentSpawn.position;
        transform.rotation = currentSpawn.rotation;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Damage/health test only
        //-----------------------------------------------------------
        if (other.CompareTag("Harmful"))
        {
            if (gameController.health > 0)
            {
                gameController.health -= 1;
            }
            Destroy(other.gameObject);
        }
        //-----------------------------------------------------------

        if (other.CompareTag("Pick_Up"))
        {
            Debug.Log("Pickup detected?");
            Pick_Up item = other.GetComponent<Pick_Up>();

            PickUpType pickUpType = item.pickUpType;

            switch (pickUpType)
            {
                case PickUpType.NONE:
                    Debug.LogWarning("PickUpType not set!");
                    break;
                case PickUpType.ACORN:
                    //Count ACORN;
                    gameController.acorns += 1;
                    Destroy(other.gameObject);
                    break;
                case PickUpType.MULCH:
                    //Count MULCH
                    gameController.mulch += 1;
                    Destroy(other.gameObject);
                    break;
                case PickUpType.HEALTH:
                    //Add HEALTH
                    if (gameController.health < 3)
                    {
                        gameController.health += 1;
                    }
                    Destroy(other.gameObject);
                    break;
                default:
                    Debug.LogWarning("PickUpType Error.");
                    break;
            }

        }
        if (other.CompareTag("Spawn_Volume"))
        {
            Debug.Log("Collider detected?");
            Spawn_Volume volume = other.GetComponent<Spawn_Volume>();

            //SpawnType spawnType = SpawnType.NONE;
            SpawnType spawnType = volume.spawnType;

            switch (spawnType)
            {
                case SpawnType.NONE:
                    Debug.LogWarning("SpawnType not set!");
                    break;
                case SpawnType.START:
                    break;
                case SpawnType.RESPAWN:
                    currentSpawn = volume.spawnPoint.transform;
                    break;
                case SpawnType.KILL:
                    Respawn();
                    break;
                default:
                    Debug.LogWarning("SpawnType Error.");
                    break;
            }
        }
    }

    //[ExecuteInEditMode]
    private void OnDrawGizmosSelected()
    {
        if (interactionDetector != null)
        {
            interactionDetector.radius = interactionRange;
        }
        

        /*
        if (interactionRange_Visibililty)
        {
            Gizmos.DrawWireSphere(transform.position + interactionDetection.center, interactionRange);
        }
        */
    }

}

//public enum SpawnType { NONE, START, RESPAWN, KILL }
//public enum SpawnType { NONE, START, SINGLE_RESPAWN, REGION_RESPAWN, SINGLE_KILL, REGION_KILL }
