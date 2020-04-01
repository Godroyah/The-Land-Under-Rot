using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityGlow : MonoBehaviour
{
    public Transform player;
    public Light glowLight;


    // Distance from player to the glowing object
    private float glowDistance;

    // base light intensity, will be intensity when player is at distance 0
    public float glowBaseIntensity;

    // How far the light reaches, and therefor detects
    private float glowRadius;

    // the subtractive percentage of how intensely the light glows
    private float glowMultiplier;

    // pseudocode from samurai punk
    //float glow = saturate(distance(worldPos, playerPos) / radius);
    //o.Emission = (1-glow) *glowColor* glowAmount;

    void Start()
    {
        glowMultiplier = 0;
        glowRadius = glowLight.range;
    }

    // Update is called once per frame
    void Update()
    {
        
        glowDistance = Vector3.Distance(transform.position, player.position);
        glowMultiplier = (glowDistance / glowRadius);
        glowLight.intensity = (glowBaseIntensity * (1 - glowMultiplier));
    }
}