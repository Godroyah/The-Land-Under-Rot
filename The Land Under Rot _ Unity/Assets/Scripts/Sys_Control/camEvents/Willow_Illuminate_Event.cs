using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willow_Illuminate_Event : Event_Type
{
    public Yellow_GazeGrowth[] yellowGazeGrowths;
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
        foreach (Yellow_GazeGrowth gazeGrowth in yellowGazeGrowths)
        {
            gazeGrowth.duration = 0;
            gazeGrowth.overrideDuration = true;

            yield return new WaitForSeconds(dissipationDelay);

            gazeGrowth.Interact();

            yield return null;
        }
    }
}
