using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Interactable
{

    GameController gameController;
    public GameObject barkContainer;
    ObjectPreferences objPrefs;
    ParticleSystem playerParticles;
    ParticleSystem barkParticles;

    private void Start()
    {
        objPrefs = GetComponent<ObjectPreferences>();
        if(objPrefs != null)
        {
            playerParticles = objPreferences.headbutt_ParticleEffect_player.GetComponent<ParticleSystem>();
            barkParticles = objPreferences.headbutt_ParticleEffect_obj.GetComponent<ParticleSystem>();
        }
        

        #region GameController Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();

            if (gameController == null)
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
            Debug.LogWarning("Could not find GameController.");

        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        // TODO: Switch from Destroy to Particle/Anim

        playerParticles.Play();
        barkParticles.Play();
        Destroy(barkContainer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
            //gameController.playerController.headbuttables.Add(this);
        }
    }



    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Headbutt"))
    //    {
    //        gameController.playerController.headbuttables.Remove(this);
    //    }
    //}
}
