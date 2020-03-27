using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_GazeGrowth : GazeGrowth
{
    public float duration = 1f;

    private float opacity = 1f;
    private float timerValue = 0f;
    private Coroutine timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (opacity == 0f)
        {
            if (timer != null)
                StopCoroutine(timer);


            timer = StartCoroutine(Timer(duration));
        }
    }

    public override void Interact()
    {
        base.Interact();

        // TODO: Anim Triggers
        /*
        animator.SetTrigger(GG_Anim.Gaze_Hit_Trigger.ToString());
        animator.SetBool(GG_Anim.Gaze_Cry_Bool.ToString(), true);
        */

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
