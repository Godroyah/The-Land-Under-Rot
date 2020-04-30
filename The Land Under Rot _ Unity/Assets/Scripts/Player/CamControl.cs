using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Camera myCamera;
    public GameObject target;

    public GameObject cameraBlinder;
    private MeshRenderer camMeshRenderer;
    private Coroutine newTransition;
    private Coroutine currentTransition;
    [Tooltip("This sets how long it takes for the blinder to adjust.")]
    public float transitionDuration = 1;

    private Transform rotationTarget;
    GameController gameController;

    private bool invertPitch = false;
    public float sensitivity_X;
    //10
    public float sensitivity_Y;
    //10
    private float horizontalInput;
    private float verticalInput;

    public Vector2 pitchMinMax = new Vector2(-30, 85);
    public float acceleration = .12f;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public bool lockPosition = false;

    float yaw;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        gameController.camControl = this;
        sensitivity_X = gameController.lookSensitivityX;
        sensitivity_Y = gameController.lookSensitivityY;
        if (myCamera == null)
        {
            Debug.LogWarning("No assigned camera found. Defaulting to MAIN");
            myCamera = gameController.mainCamera;
        }

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        currentRotation = transform.eulerAngles;

        // Blinder stuffs
        camMeshRenderer = cameraBlinder.GetComponent<MeshRenderer>();
        camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        
    }

    private void FixedUpdate()
    {
        if (!lockPosition)
        {
            Move();
        }


    }

    private void LateUpdate()
    {
        if (!lockPosition)
        {
            transform.position = target.transform.position;
        }

        // Seems like over correction to me
        // JK THIS IS HIGHLY NEEDED
        rotationTarget.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Mouse X");
        verticalInput = Input.GetAxisRaw("Mouse Y");
    }

    private void Move()
    {
        // <<-----------------------------------------------------------------------------**
        // Get input data and clamp 
        // **---------------------------------------------**

        yaw += Input.GetAxis("Mouse X") * sensitivity_X;

        if (invertPitch)
        {
            pitch += Input.GetAxis("Mouse Y") * sensitivity_Y;
        }

        else
        {
            pitch += Input.GetAxis("Mouse Y") * -sensitivity_Y;
        }

        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Calculate rotation and apply to camera
        // **---------------------------------------------**

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, acceleration * Time.deltaTime);

        transform.eulerAngles = currentRotation;

        // **----------------------------------------------------------------------------->>
    }

    public void SetCameraTarget(Transform target)
    {
        rotationTarget = target;
    }

    #region Camera Blinder
    IEnumerator LerpAlpha(float percent)
    {
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }
        currentTransition = newTransition;

        float time = 0;
        float originalValue = camMeshRenderer.material.GetColor("_BaseColor").a;

        while (time <= transitionDuration)
        {
            yield return new WaitForEndOfFrame();
            float newAlpha = Mathf.Lerp(originalValue, percent, time / transitionDuration);

            camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, newAlpha));
            time += Time.deltaTime;
        }

        camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, percent));
    }

    public void AdjustBlinder(float percent)
    {
        newTransition = StartCoroutine(LerpAlpha(percent));
    }

    public void RemoveBlinder()
    {
        Debug.Log("Removing Blinder");
        AdjustBlinder(0);
    }
    #endregion
}
