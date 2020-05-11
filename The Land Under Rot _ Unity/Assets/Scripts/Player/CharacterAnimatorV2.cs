using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorV2 : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    Animator animator;
    float inputValue;
    bool justMoved;
    bool isFalling = false;
    public GameObject Seedling;

    public WalkSurface surfaceType = WalkSurface.NONE;
    public ParticleSystem runParticleSystem;

    // Start is called before the first frame update

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = playerController.Rb;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.enabled == true)
            if (!playerController.StopPlayer)
            {
                #region Jumping

                if (playerController.ShouldJump)
                {
                    AudioManager.Instance.Play_Jump();
                }

                //faster falling
                if (/*rb.velocity.y < 0 && */!playerController.IsGrounded)
                {
                    animator.SetBool(CharAnimation.Is_Falling_Bool.ToString(), true);
                    //AudioManager.Instance.Play_Falling();    
                }
                    
                //else if (rb.velocity.y > 0 && !playerController.ShouldJump)
                //animator.SetBool("Airborne_Bool", true);



                //if (!animator.GetBool("Airborne_Bool") && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
                //    animator.SetTrigger("Run_Button_Trigger");

               #endregion


                if ((Mathf.Abs(playerController.HorizontalInput) > 0.1f || Mathf.Abs(playerController.VerticalInput) > 0.1f))
                {
                    animator.SetBool(CharAnimation.Is_Walking_Bool.ToString(), true);
                    if (!runParticleSystem.isPlaying && playerController.IsGrounded)
                    {
                        runParticleSystem.Play();
                        AudioManager.Instance.Play_ClothesRustle();
                      //  AudioManager.Instance.Play_Walk(surfaceType);
                        //   Seedling.GetComponent<PlayerSounds>().footstepPlay();

                    }
                    else if (runParticleSystem.isPlaying && !playerController.IsGrounded)
                    {
                        runParticleSystem.Stop();
                    }

                }
                else
                {
                    animator.SetBool(CharAnimation.Is_Walking_Bool.ToString(), false);
                    if (runParticleSystem.isPlaying)
                    {
                        runParticleSystem.Stop();
                    }
                }


                if (playerController.ShouldHeadbutt && playerController.HeadbuttCoroutine == null)
                {
                    animator.SetTrigger(CharAnimation.Headbutt_Trigger.ToString());
                    AudioManager.Instance.Play_Headbutt();
                }


            }
            else
            {
                animator.SetBool(CharAnimation.Is_Walking_Bool.ToString(), false);
                if (runParticleSystem.isPlaying)
                {
                    runParticleSystem.Stop();
                }
            }

        if (playerController.IsGrounded)
        {
            animator.SetBool(CharAnimation.Is_Falling_Bool.ToString(), false);
            if (isFalling)
            {
                isFalling = false;
                AudioManager.Instance.Play_Landing_OnFeet();
            }
            
        }
        else
        {
            isFalling = true;
            AudioManager.Instance.Play_Falling();
            if (runParticleSystem.isPlaying)
            {
                runParticleSystem.Stop();
            }
        }

    }
    

}



public enum CharAnimation
{
    Headbutt_Trigger,
    //Run_Start_Trigger,
    //Jump_Start_Trigger,
    //Idle_Break_Trigger, // not set up yet
    Is_Falling_Bool,
    //Is_Drowning_Bool, // not set up yet
    Is_Walking_Bool,
    //Is_Jumping_Bool,
    //Is_Sprinting_Bool // not set up yet
}
 