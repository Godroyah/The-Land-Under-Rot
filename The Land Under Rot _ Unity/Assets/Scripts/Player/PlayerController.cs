﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    #region Player Components
    //public GameObject playerCamera;
    private Animator animator;
    public CamControl camControl;
    public Rigidbody Rb { get; private set; }
    //Will eventually search spawner out by scene
    public Transform currentSpawn;
    //public Transform playerHolder;
    #endregion

    [Header("Player Modifiers")]
    #region Modifiers
    public int health = 3;

    [Header("Player Inventory")]
    #region Inventory
    public int acorns;
    public int mulch;
    #endregion

    [Range(1, 50)]
    public float moveVelocity = 12f;

    [Range(1, 50)]
    public float runVelocity = 20f;

    float maxVelocityChange = 10f;

    [Range(0, 200), Tooltip("Controls how fast the obj rotates when changing directions.")]
    public float rotationSpeed = 0.1f;

    [Range(0, 5), Tooltip("Controls how quickly fade out occurs after dying.")]
    public float fadeDelay;

    [Space(10)] // Adds literal space in the inspector

    #region Jumping Variables

    [Range(1, 50)]
    public float jumpVelocity = 10f;

    //public float deathfadeDelay;

    [Range(0.001f, 50), Tooltip("Controls how fast the Player will fall.")]
    public float fallMultiplier = 2.5f; // TODO: Look at this for GLIDING later

    [Range(0.001f, 50), Tooltip("Controls how high the player jumps on a shorter press.")]
    public float lowJumpMultiplier = 2f;

    #endregion

    [Space(10)]// Adds literal space in the inspector

    //public bool interactionRange_Visibililty = false;
    [Range(0f, 5f), Tooltip("Determines the Radius for Interactions.")]
    public float interactionRange = 1f;

    [Tooltip("This field helps specify that the SphereCollider on the Player is used for Interaction Detection.")]
    //public SphereCollider interactionDetector; // TODO: Will this sphere collider cause issues?
    public MeshCollider interactionDetector;
    public SphereCollider headButtDetector;
    public Transform groundChecker;
    public Transform collisionChecker;

    private int playerLayerMask;
    #endregion


    //public CapsuleCollider headButt
    //[Header("Camera Override")]


    [Header("Booleans")]
    #region Bools
    public bool isDead;
    public bool infiniteJumping = false;
    public bool ShouldRun { get; private set; }
    public bool ShouldInteract { get; private set; }
    public bool ShouldJump { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool ShouldHeadbutt { get; private set; }
    public bool JustHeadButted { get; private set; }
    public bool StopPlayer { get; set; }
    #endregion


    [Header("Input")]
    #region Input

    [Range(0, 1), Tooltip("Cutoff for the value when the input is accepted.")]
    public float inputDelay = 0.1f;

    [Range(0, 1), Tooltip("Delay until the next time 'Interact' is available.")]
    public float interactDelay = 0.1f;
    private Coroutine interactingCoroutine;

    [Range(0, 1), Tooltip("Delay until the 'Headbutt' swings forward.")]
    public float headbuttStartDelay = 1.0f;
    [Range(0, 1), Tooltip("Delay until the next time 'Headbutt' is available.")]
    public float headbuttFinishDelay = 0.1f;
    //public float headbuttDelay = 0.1f;
    public Coroutine HeadbuttCoroutine { get; private set; }

    [Space(5)]

    [Range(0.001f, 1), Tooltip("Determines how responsive the obj is to movement.")]
    public float rotationTargetDist = 0.1f;

    [Range(0, 1), Tooltip("Cutoff distance for when the obj will rotate. This will be capped by rotationTargetDist as a percentage.")]
    public float rotationCutoff = 0.1f;
    [HideInInspector]
    public Vector2 controlInput;
    public float HeadbuttInput { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    private Quaternion targetRotation;
    private Transform rotationTarget;

    public List<Interactable> interactables = new List<Interactable>();
    public List<Interactable> headbuttables = new List<Interactable>();
    #endregion

    
    public GameObject fadePane;
    private Fade_Done fadeDone;
    private Animator fadeAnim;
    private Interactable currentTarget = null;

    private void Awake()
    {
        fadePane = GameObject.Find("FadePane");

        if (fadePane != null)
        {
            fadeDone = fadePane.GetComponent<Fade_Done>();
            fadeAnim = fadePane.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("FadePane missing from scene!");
        }


        if (!GetComponent<Rigidbody>())
            Debug.LogWarning("Rigidbody missing on " + gameObject.name);
        else
            Rb = GetComponent<Rigidbody>();

        if (camControl == null)
            Debug.LogWarning("Camera Controller Missing!!");

        if (animator != null)
            animator = GetComponent<Animator>();

        headButtDetector.enabled = false;

        //Is the whole check redundant since the detector is on its own object now/just reworked to grab component on object instead of just component?
        if (interactionDetector == null)
        {
            //interactionDetector = GetComponent<SphereCollider>();
            interactionDetector = GetComponent<MeshCollider>();

            //Redundant now that we're using a Mesh Collider?
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
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.playerController = this;

        JustHeadButted = true;
        // TODO: Find Spawn Point
        isDead = false;

        if (currentSpawn == null)
        {
            currentSpawn = transform;
        }

        this.transform.position = currentSpawn.position;
        this.transform.rotation = currentSpawn.rotation;

        // Bit shift the index of the layer (8) to get a bit mask
        playerLayerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        playerLayerMask = ~playerLayerMask;
    }

    // Update is called once per frame
    void Update()
    {
        Reset();
        GetInput();

        IsGrounded = Physics.CheckSphere(groundChecker.position, 0.4f, playerLayerMask, QueryTriggerInteraction.Ignore);
        //IsGrounded = Physics.CheckBox(groundChecker.position, new Vector3(0.75f, 0.25f, 0.75f), transform.rotation, playerLayerMask, QueryTriggerInteraction.Ignore);

        #region Interactable Check

        //Redefine above


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

                    /*
                    if (hit.collider.gameObject != interactables[targetIndex].gameObject) // If the targeted gameObject wasn't the object hit
                        continue; // If it doesn't hit, it doesn't see it
                    */

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
                        ToggleHighlight(currentTarget, false);
                    }
                    currentTarget = closestTarget;
                }
            }
            /*
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
            }*/
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

        #endregion


        // TODO: Limit this to happen if outside of specific range
        //       Maybe if no input don't move rotationTarget?
        if (Vector3.Distance(transform.position, rotationTarget.position) > (rotationCutoff * rotationTargetDist))
        {
            Rotate();
        }
        else
            Rb.angularVelocity = Vector3.zero;


        #region Jumping

        //faster falling
        if (Rb.velocity.y < 0f)
            Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (Rb.velocity.y > 0 && !ShouldJump)
            Rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        if (infiniteJumping)
        {
            IsGrounded = true;
        }

        if (ShouldJump && IsGrounded && !StopPlayer)
        {
            Rb.velocity = Vector3.up * jumpVelocity;

            IsGrounded = false;
        }

        #endregion

        if (ShouldInteract && interactingCoroutine == null && !StopPlayer)
        {
            StartCoroutine(Interacting());
        }

        if (ShouldHeadbutt && HeadbuttCoroutine == null && !StopPlayer)
        {
            StartCoroutine(Headbutting());
        }
    }

    private void FixedUpdate()
    {
        if (!StopPlayer)
        {
            Move();
        }
        else
            rotationTarget.position = transform.position;

    }

    private void GetInput()
    {
        if (!isDead)
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Input.GetAxis("Vertical");
            ShouldRun = Input.GetKey(KeyCode.LeftShift);
            ShouldJump = Input.GetButtonDown("Jump");
            ShouldInteract = Input.GetButtonDown("Interact");
            ShouldHeadbutt = Input.GetButtonDown("Headbutt");
            HeadbuttInput = Input.GetAxis("Headbutt");
        }

        if (HeadbuttInput > 0.1)
        {
            if (JustHeadButted)
            {
                ShouldHeadbutt = true;
            }
            JustHeadButted = false;
        }
        else
        {
            JustHeadButted = true;
        }
    }

    private void ToggleHighlight(Interactable focus, bool state)
    {
        if (focus.billboard_UI != null)
        {
            focus.billboard_UI.SetActive(state);
        }
    }

    IEnumerator Interacting()
    {
        if (currentTarget != null)
        {
            currentTarget.Interact();
        }

        yield return new WaitForSeconds(interactDelay);
        interactingCoroutine = null;
    }

    IEnumerator Headbutting()
    {
        yield return new WaitForSeconds(headbuttStartDelay);
        headButtDetector.enabled = true;
        yield return new WaitForSeconds(headbuttFinishDelay);
        headButtDetector.enabled = false;
        HeadbuttCoroutine = null;
    }

    private void Move()
    {
        // Don't start accepting input until it peaks the inputDelay
        if (Mathf.Abs(HorizontalInput) < inputDelay)
            HorizontalInput = 0;
        if (Mathf.Abs(VerticalInput) < inputDelay)
            VerticalInput = 0;

        bool hasCollided = Physics.CheckBox(collisionChecker.position, new Vector3(0.75f, 1.75f, 0.75f), transform.rotation, playerLayerMask, QueryTriggerInteraction.Ignore);

        Vector3 movement;

        if (!IsGrounded && hasCollided)
        {
            //Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            movement = Vector3.zero;
            //movement = new Vector3(HorizontalInput, 0, VerticalInput) * 0.5f;
            //movement = Vector3.down;
        }
        else
            movement = new Vector3(HorizontalInput, 0, VerticalInput);

        Vector3 tempDir = rotationTarget.TransformDirection(movement * rotationTargetDist);

        rotationTarget.position = tempDir + transform.position;



        //bool hasCollided = Physics.CheckCapsule(temp, 1.77f, playerLayerMask, QueryTriggerInteraction.Ignore);


        //Debug.Log(hasCollided);
        //if (IsGrounded && !hasCollided)
        //{
        float targetSpeed;
        if (ShouldRun)
            targetSpeed = runVelocity;
        else
            targetSpeed = moveVelocity;


        movement = rotationTarget.TransformDirection(movement) * targetSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = Rb.velocity;
        Vector3 velocityChange = (movement - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        //velocityChange.y = 0;
        velocityChange.y = Physics.gravity.y/ 10;

        Rb.AddForce(velocityChange * Time.deltaTime * 50, ForceMode.VelocityChange);
        //}

    }

    private void Rotate()
    {
        //find the vector pointing from our position to the target
        Vector3 _direction = (rotationTarget.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed); // For smoother transitions the speed should increase if the distance is shorter

    }

    public void KillPlayer()
    {
        if (fadePane != null)
            StartCoroutine("FadeOut");
        else
        {
            transform.position = currentSpawn.position;
            transform.rotation = currentSpawn.rotation;
            Rb.velocity = Vector3.zero;
        }
    }

    IEnumerator FadeOut()
    {
        isDead = true;
        camControl.lockPosition = true;
        HorizontalInput = 0;
        VerticalInput = 0;

        yield return new WaitForSeconds(fadeDelay);

        fadeAnim.SetTrigger("FadeOut");
    }

    public void Reset()
    {
        if (fadePane != null && fadeDone.fadeOver)
        {
            transform.position = currentSpawn.position;
            transform.rotation = currentSpawn.rotation;
            Rb.velocity = Vector3.zero;
            health = 3;
            isDead = false;
            fadeAnim.ResetTrigger("FadeOut");
            fadeAnim.SetTrigger("FadeIn");
            fadeDone.fadeOver = false;
            camControl.lockPosition = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextScene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}


