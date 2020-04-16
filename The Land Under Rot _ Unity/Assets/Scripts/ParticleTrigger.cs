using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void Interact()
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
        else if (particleSystem.isPlaying)
        {
            particleSystem.Stop();
            particleSystem.Play();
        }
    }
}
