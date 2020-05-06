using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbuttSFX : MonoBehaviour
{
    public HeadbuttSurface hitType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            AudioManager.Instance.Play_Headbutt(hitType);
        }
    }
}
