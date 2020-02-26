using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    Animator animator;
    float inputValue;
    bool justMoved;

    public float landDistance = 2f;

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
        {
            #region Jumping

            if (playerController.ShouldJump)
            {
                animator.SetTrigger("Jump_Button_Trigger");
            }

            //faster falling
            if (/*rb.velocity.y < 0 && */!playerController.IsGrounded)
                animator.SetBool("Airborne_Bool", true);
            //else if (rb.velocity.y > 0 && !playerController.ShouldJump)
            //animator.SetBool("Airborne_Bool", true);

            if (playerController.IsGrounded)
            {
                animator.SetBool("Airborne_Bool", false);
            }

            //if (!animator.GetBool("Airborne_Bool") && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
            //    animator.SetTrigger("Run_Button_Trigger");

            if (!animator.GetBool("Airborne_Bool") && (Mathf.Abs(playerController.HorizontalInput) > 0.1f || Mathf.Abs(playerController.VerticalInput) > 0.1f))
            {
                if (!justMoved)
                {
                    animator.SetTrigger("Run_Button_Trigger");
                }
                justMoved = true;

            }
            else
                justMoved = false;





            if (Mathf.Abs(playerController.HorizontalInput) > 0.1f || Mathf.Abs(playerController.VerticalInput) > 0.1f)
                animator.SetBool("Holding_Run_Bool", true);
            else
                animator.SetBool("Holding_Run_Bool", false);



            if (playerController.ShouldHeadbutt && playerController.HeadbuttCoroutine == null)
            {
                animator.SetTrigger("Headbutt_Button_Trigger");
            }

            #endregion
        }

    }
}
