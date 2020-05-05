using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Interactable
{
    public Interaction interactionObject;
    //public bool isEvent;
    GameController gameController;
    public GameObject barkContainer;
    //ObjectPreferences objPrefs;
    ParticleSystem playerParticles;
    ParticleSystem barkParticles;
    BoxCollider thisTrigger;
    
    //public GameObject eventBark;
    //Temporary until EventTrigger script is in place

    //Gong gongControl;

    private void Start()
    {
        //Remove once new EventTrigger script is in place
        //if(isEvent)
        //{
        //    gongControl = eventBark.GetComponent<Gong>();
        //}
        thisTrigger = GetComponent<BoxCollider>();

        //objPrefs = GetComponent<ObjectPreferences>();
        if(objPrefs != null)
        {
            playerParticles = objPrefs.headbutt_ParticleEffect_player.GetComponent<ParticleSystem>();
            barkParticles = objPrefs.headbutt_ParticleEffect_obj.GetComponent<ParticleSystem>();
        }

        gameController = GameController.Instance;
        GameController.Instance.onLevelLoaded += UpdateOnLevelLoad;
    }

    public override void Interact()
    {
        base.Interact();

        // TODO: Switch from Destroy to Particle/Anim

        playerParticles.Play();
        barkParticles.Play();
        thisTrigger.enabled = false;
        //if(isEvent)
        //{
        //    gongControl.Interact();
        //}
        Destroy(barkContainer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }

    public void UpdateOnLevelLoad()
    {
        if (GameController.Instance.HasInteracted(interactionObject))
        {
            thisTrigger.enabled = false;
            Destroy(barkContainer);
        }
    }

}
