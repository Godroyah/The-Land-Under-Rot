using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteParticles : MonoBehaviour
{
    public Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    [Header("Emote Particles")]
    #region Emote Particles
    public ParticleSystem Sleeping;
    public ParticleSystem Waiting;
    public ParticleSystem Exclamation;
    public ParticleSystem Angry;
    public ParticleSystem Shocked;
    public ParticleSystem Confused;
    public ParticleSystem Dizzy;
    public ParticleSystem Happy;
    #endregion

}
