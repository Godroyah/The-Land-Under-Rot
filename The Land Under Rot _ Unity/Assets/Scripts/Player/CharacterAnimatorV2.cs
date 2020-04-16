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
                    animator.SetTrigger(CharAnimation.Jump_Start_Trigger.ToString());
                    animator.SetBool(CharAnimation.Is_Jumping_Bool.ToString(), true);
                }

                //faster falling
                if (/*rb.velocity.y < 0 && */!playerController.IsGrounded)
                    animator.SetBool(CharAnimation.Is_Falling_Bool.ToString(), true);
                //else if (rb.velocity.y > 0 && !playerController.ShouldJump)
                //animator.SetBool("Airborne_Bool", true);



                //if (!animator.GetBool("Airborne_Bool") && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
                //    animator.SetTrigger("Run_Button_Trigger");

                if (!animator.GetBool(CharAnimation.Is_Falling_Bool.ToString()) && (Mathf.Abs(playerController.HorizontalInput) > 0.1f || Mathf.Abs(playerController.VerticalInput) > 0.1f))
                {
                    if (!justMoved)
                    {
                        animator.SetTrigger(CharAnimation.Run_Start_Trigger.ToString());
                    }
                    justMoved = true;

                }
                else
                    justMoved = false;


                #endregion


                if ((Mathf.Abs(playerController.HorizontalInput) > 0.1f || Mathf.Abs(playerController.VerticalInput) > 0.1f))
                {
                    animator.SetBool(CharAnimation.Is_Walking_Bool.ToString(), true);
                    if (!runParticleSystem.isPlaying && playerController.IsGrounded)
                    {
                        runParticleSystem.Play();
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
                }


            }
            else
            {
                animator.SetBool(CharAnimation.Is_Walking_Bool.ToString(), false);
            }

        if (playerController.IsGrounded)
        {
            animator.SetBool(CharAnimation.Is_Falling_Bool.ToString(), false);
        }

    }

    IEnumerator IsJumpingCheck()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump V2") == false) // '0' references the anim layer
            {
                animator.SetBool(CharAnimation.Is_Jumping_Bool.ToString(), false);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

public enum CharAnimation
{
    Headbutt_Trigger,
    Run_Start_Trigger,
    Jump_Start_Trigger,
    Idle_Break_Trigger, // not set up yet
    Is_Falling_Bool,
    Is_Drowning_Bool, // not set up yet
    Is_Walking_Bool,
    Is_Jumping_Bool,
    Is_Sprinting_Bool // not set up yet
}