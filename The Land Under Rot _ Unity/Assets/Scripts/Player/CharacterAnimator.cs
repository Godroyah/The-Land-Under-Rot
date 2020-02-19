using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    Animator animator;

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
        #region Jumping

        if (playerController.ShouldJump)
        {
            animator.SetTrigger("Jump_Button_Trigger");
        }

        //faster falling
        if (rb.velocity.y < 0)
            animator.SetBool("Airborne_Bool", true);
        //else if (rb.velocity.y > 0 && !playerController.ShouldJump)
        //animator.SetBool("Airborne_Bool", true);

        if (!playerController.CanJump)
        {
            #region Raycast Layer Masking
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            #endregion
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5f))
            {
                if (hit.distance < landDistance)
                {
                    animator.SetBool("Airborne_Bool", false);
                }
            }
        }

        if (!animator.GetBool("Airborne_Bool") && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
            animator.SetTrigger("Run_Button_Trigger");


        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
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
