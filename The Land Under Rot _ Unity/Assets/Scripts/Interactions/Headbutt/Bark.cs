using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Interactable
{
    public bool isEvent;
    GameController gameController;
    public GameObject barkContainer;
    ObjectPreferences objPrefs;
    ParticleSystem playerParticles;
    ParticleSystem barkParticles;
    public GameObject eventBark;
    Gong gongControl;

    private void Start()
    {
        if(isEvent)
        {
            gongControl = eventBark.GetComponent<Gong>();
        }

        objPrefs = GetComponent<ObjectPreferences>();
        if(objPrefs != null)
        {
            playerParticles = objPrefs.headbutt_ParticleEffect_player.GetComponent<ParticleSystem>();
            barkParticles = objPrefs.headbutt_ParticleEffect_obj.GetComponent<ParticleSystem>();
        }

        gameController = GameController.Instance;
    }

    public override void Interact()
    {
        base.Interact();

        // TODO: Switch from Destroy to Particle/Anim

        playerParticles.Play();
        barkParticles.Play();
        if(isEvent)
        {
            gongControl.Interact();
        }
        Destroy(barkContainer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }

}
