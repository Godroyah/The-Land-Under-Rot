using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MulchType { NONE, ANGEL, STAR, WILLOW }

public class Mulch : Interactable
{

    #region OldVariables
    //public GameObject[] objsToSpawn;

    //public Transform[] spawnPoints;

    //public float spawnJumpHeight;

    //private int numberSpawned;

    //private float itemDirectionX;
    //private float itemDirectionZ;

    //private float directionControllerX;
    //private float directionControllerZ;

    //public GameObject mulchContainer;

    // public Collider mulchCollider;
    #endregion

    public MulchType mulchType;

    public MeshRenderer fullMulch;

    public MeshRenderer depletedMulch;

    public bool isTutorial;

    GameController gameController;

    //ParticleSystem playerParticles;
    ParticleSystem mulchParticles;

    public string mulchMessage;

    public GameObject youGotMulch;

    Mulch_Collected mulchCollected;

    //bool hasPlayed = false;

    private void Start()
    {
        gameController = GameController.Instance;

        if (objPrefs != null)
        {
            mulchParticles = objPrefs.headbutt_ParticleEffect_obj.GetComponent<ParticleSystem>();
        }
    }

    public override void Interact()
    {
        base.Interact();

        if(!isTutorial && gameController.mulchant_GivenBottles)
        {
            switch(mulchType)
            {
                case MulchType.NONE:
                    Debug.LogWarning("Mulch type is not set! Cannot progress!");
                    break;
                case MulchType.ANGEL:
                    if(gameController.mulchant_GivenBottles)
                    {
                        gameController.hasBrownMulch = true;
                        gameController.FillBrownBottle();
                    }
                    break;
                case MulchType.STAR:
                    gameController.hasGreenMulch = true;
                    gameController.FillGreenBottle();
                    break;
                case MulchType.WILLOW:
                    gameController.hasYellowMulch = true;
                    gameController.FillYellowBottle();
                    break;
                default:
                    Debug.LogWarning("MulchType Error.");
                    break;
            }
            if(!depletedMulch.enabled)
            {
                mulchParticles.Play();
                ActivateMessage();
                fullMulch.enabled = false;
                depletedMulch.enabled = true;
            }
        }

        #region OldFunctionality
        //if(!isTutorial)
        //{
        //    mulchCollider.enabled = false;
        //    if (objsToSpawn.Length == spawnPoints.Length)
        //    {
        //        foreach (GameObject obj in objsToSpawn)
        //        {
        //            GameObject temp;
        //            temp = Instantiate(obj, transform) as GameObject;

        //            temp.transform.position = spawnPoints[numberSpawned].position;

        //            //Randomize Spawning - Revisit
        //            #region
        //            //if(randomSpawn == spawnStart)
        //            // {
        //            //     temp.transform.position = randomSpawn;
        //            // }
        //            //else
        //            // {
        //            //     randomSpawn = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), Random.Range(spawnBounds.min.y, spawnBounds.max.y), Random.Range(spawnBounds.min.z, spawnBounds.max.z));
        //            // }

        //            //if(randomSpawn == Vector3.zero)
        //            //{
        //            //    randomSpawn = new Vector3(Random.Range )
        //            //}

        //            //temp.transform.position = new Vector3(Random.Range())

        //            //transform.position;
        //            #endregion  //Randomize spawning

        //            Rigidbody rb;
        //            rb = temp.GetComponentInChildren<Rigidbody>();

        //            if (rb == null) // TODO: Mulch Spawn Rigidbodies
        //                rb = temp.AddComponent<Rigidbody>();

        //            directionControllerX = Random.Range(0, 1);
        //            directionControllerZ = Random.Range(0, 1);

        //            if (directionControllerX > 0.5)
        //            {
        //                itemDirectionX = 100f;
        //            }
        //            else
        //            {
        //                itemDirectionX = -100f;
        //            }

        //            if (directionControllerZ > 0.5)
        //            {
        //                itemDirectionZ = 100f;
        //            }
        //            else
        //            {
        //                itemDirectionZ = -100f;
        //            }

        //            temp.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        //            rb.AddForce((new Vector3(Random.Range(-1, 1) * itemDirectionX, 0, Random.Range(-1, 1) * itemDirectionZ) + mulchContainer.transform.up * spawnJumpHeight));

        //            numberSpawned += 1;
        //        }

        //        if (numberSpawned >= objsToSpawn.Length)
        //        {
        //            transform.DetachChildren();
        //            Destroy(mulchContainer);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Objs To Spawn and Spawn Points arrays on Mulch do not have the same number of elements!");
        //    }
        //}
        #endregion
    }

    void ActivateMessage()
    {
        mulchCollected = youGotMulch.GetComponent<Mulch_Collected>();
        mulchCollected.mulch_text.text = mulchMessage;
        //mulchCollected.textLength = mulchMessage.Length;
        //mulchCollected.SetName(mulchMessage);
        GameObject mulchAnnouncement = Instantiate(youGotMulch);
       // mulchCollected.GotMulch();
        //hasPlayed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
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
