using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 4f;
    [Tooltip("Value used to smooth the camera sliding in/out")]
    public float smoothing = 10f;
    Vector3 dollyDir;
    //public Vector3 dollyDirAdjusted;
    private float distance;
    private int playerMask;

    private void Awake()
    {
        playerMask = 1 << 5 << 8;
        playerMask = ~playerMask;

        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, playerMask, QueryTriggerInteraction.Ignore))
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        else
            distance = maxDistance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smoothing);
    }
}
