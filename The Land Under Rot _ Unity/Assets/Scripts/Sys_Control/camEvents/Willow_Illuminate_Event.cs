using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willow_Illuminate_Event : Event_Type
{
    public Yellow_GazeGrowth[] yellowGazeGrowths;
    public Event_Trigger[] yellowEvents;
    public float dissipationDelay;

    //private void Start()
    //{
        
    //}

    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(PermaFade());
    }


    IEnumerator PermaFade()
    {
        //for(int i = 0; i < yellowEvents.Length; i++)
        //{
        //    yellowEvents[i].enabled = false;
        //    yield return null;
        //}
        foreach (Yellow_GazeGrowth gazeGrowth in yellowGazeGrowths)
        {
            gazeGrowth.usesCamEvent = false;
            gazeGrowth.duration = 0;
            gazeGrowth.overrideDuration = true;

            yield return new WaitForSeconds(dissipationDelay);

            gazeGrowth.Interact();

            yield return null;
        }
    }
}
