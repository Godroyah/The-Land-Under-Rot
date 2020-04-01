using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloFade : MonoBehaviour
{
    public Transform player;
    private float haloDistance;
    private float haloRadius;
    private float haloMultiplier;
    private float haloSize;
    public Light glowLight;

    // Start is called before the first frame update
    void Start()
    {
        haloRadius = glowLight.range;
    }

    // Update is called once per frame
    void Update()
    {
 
        haloDistance = Vector3.Distance(transform.position, player.position);

        haloMultiplier = (haloDistance / haloRadius);

        haloSize = (1 - haloMultiplier);

        if (haloSize <= 0)
        {
            transform.localScale = new Vector3(0, 1, 0);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(haloSize, 1, haloSize);
        }
    }
}
