using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PickUpType { NONE, ACORN, MULCH, HEALTH, HARMFUL}
public enum PickUpType { NONE, ACORN}

public class Pick_Up : MonoBehaviour
{
    public PickUpType pickUpType; //Drop down menu for the types of pickups (Acorn, Mulch, etc.)'
    //private Collider mulchCollider;
    private ObjectPreferences objPref;
    private AudioSource acornAudio;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        objPref = GetComponent<ObjectPreferences>();
        acornAudio = GetComponent<AudioSource>();

        //if (pickUpType == PickUpType.MULCH)
        //{
        //    mulchCollider = GetComponent<Collider>();
        //    StartCoroutine(KillCollider());
        //}

        if(pickUpType == PickUpType.ACORN)
        {
            if (objPref != null && acornAudio != null)
            {
                acornAudio.clip = objPref.pickup_AudioClip;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if((pickUpType == PickUpType.ACORN || pickUpType == PickUpType.HEALTH || pickUpType == PickUpType.HARMFUL) && Time.timeScale > 0)
        //{
        //    transform.Rotate(0, 3, 0, Space.World);
        //}

        if (pickUpType == PickUpType.ACORN  && Time.timeScale > 0)
        {
            transform.Rotate(0, 3, 0, Space.World);
        }
    }

    IEnumerator KillCollider()
    {
        yield return new WaitForSeconds(0.1f);

        //mulchCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pick me up!");

            if (playerController == null)
            {
                playerController = other.GetComponentInParent<PlayerController>();
            }

            switch (pickUpType)
            {
                case PickUpType.NONE:
                    Debug.LogWarning("PickUpType not set!");
                    break;
                case PickUpType.ACORN:
                    //Count ACORN;
                    //move to Player; have GameController call for it
                    acornAudio.Play();
                    if(acornAudio.isPlaying)
                    {
                        Debug.Log("Playing!");
                    }
                    playerController.acorns += 1;
                    Destroy(gameObject);
                    break;
                //case PickUpType.MULCH:
                //    //Count MULCH
                //    mulchCollider.enabled = false;
                //    playerController.mulch += 1;
                //    Destroy(gameObject);
                //    break;
                //case PickUpType.HEALTH:
                //    //Add HEALTH
                //    if (playerController.health < 3)
                //    {
                //        playerController.health += 1;
                //    }
                //    Destroy(gameObject);
                //    break;
                //case PickUpType.HARMFUL:
                //    //Take HEALTH
                //    if (playerController.health > 0)
                //    {
                //        playerController.health -= 1;
                //    }
                //    Destroy(gameObject);
                //    break;
                default:
                    Debug.LogWarning("PickUpType Error.");
                    break;
            }

        }
        //else if(pickUpType == PickUpType.MULCH)
        //{
        //    mulchCollider.enabled = true;
        //}
    }
}
