using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBasedRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private int playerMask;
    public Vector3 testDirection;
    Quaternion directioning;
    Vector3 hitNormal = Vector3.up;
    Vector3 previousHitNormal = Vector3.up;

    private void Start()
    {
        playerMask = 1 << 8;
        playerMask = ~playerMask;
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Vector3 targetDirection;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, playerMask))
        {
            previousHitNormal = hitNormal.normalized; // redundant, but meh
            hitNormal = hit.normal.normalized;

            //float normalsAngle = Mathf.Acos(Vector3.Dot(previousHitNormal, hitNormal) / (previousHitNormal.magnitude * hitNormal.magnitude));
            float normalsAngle = Vector3.Dot(previousHitNormal, hitNormal);
            //if (normalsAngle > (Mathf.Deg2Rad * 5))
            if (normalsAngle > (Mathf.Deg2Rad * 5))
            {
                Debug.Log(normalsAngle);
                directioning.SetFromToRotation(transform.up.normalized, hit.normal.normalized);
            }


        }
        else
        {
            directioning.SetFromToRotation(transform.forward, gameObject.transform.parent.forward);
            previousHitNormal = Vector3.up;
        }

        /*
        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.InverseTransformVector(testDirection), targetDirection, singleStep, 0.0f);
        Vector3 newDirection = Vector3.RotateTowards(transform.up, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red, 2f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.rotation = Quaternion.LookRotation(targetDirection);
        */

        //Quaternion rotation = Quaternion.LookRotation(gameObject.transform.parent.forward.normalized, hit.normal.normalized);
        //transform.rotation = rotation;

        //directioning.SetFromToRotation(transform.up.normalized, hit.normal.normalized);
        transform.rotation = directioning * transform.rotation;

    }
}
