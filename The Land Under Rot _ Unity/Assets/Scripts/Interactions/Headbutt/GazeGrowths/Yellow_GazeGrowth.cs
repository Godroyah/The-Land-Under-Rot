using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_GazeGrowth : GazeGrowth
{

    [Space(10)]

    [Tooltip("This is the time until the GG resets and is interactable again")]
    public float duration = 1f;
    [Tooltip("This overrides the duration variables in all of the dark volumes")]
    public bool overrideDuration = true;

    public List<Darkness> darkVolumes = new List<Darkness>();

    private float timerValue = 0f;

    private void Start()
    {
        thisDetector = GetComponent<SphereCollider>();
    }

    public override void Interact()
    {
        base.Interact();

        thisDetector.enabled = false;

        if(duration > 0)
        {
            animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
            animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);
        }
        else if (duration < 0)
        {
            Debug.LogWarning("Please raise the duration about 0!");
        }

        StartCoroutine(Timer(duration));
        if (overrideDuration)
        {
            for (int i = 0; i < darkVolumes.Count; i++)
            {
                darkVolumes[i].Illuminate(duration);
            }
        }
        else
        {
            for (int i = 0; i < darkVolumes.Count; i++)
            {
                darkVolumes[i].Illuminate();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
        }
    }

    IEnumerator Timer(float a_Duration)
    {
        timerValue = a_Duration;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            timerValue -= 0.5f;

            if (timerValue <= 0)
                break;
        }

        thisDetector.enabled = true;

        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), false);
    }
}
