using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_GazeGrowth : GazeGrowth
{
    public float duration = 1f;

    public List<Darkness> darkVolumes = new List<Darkness>();

    private float opacity = 1f;
    private float timerValue = 0f;
    private Coroutine timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Interact()
    {
        base.Interact();


        animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);

        for (int i = 0; i < darkVolumes.Count; i++)
        {
            darkVolumes[i].Illuminate();
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
    }
}
