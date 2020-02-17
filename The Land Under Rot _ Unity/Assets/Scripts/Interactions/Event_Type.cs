using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum EventName { NONE, LIZARD_CART}

public class Event_Type : MonoBehaviour
{
    protected bool playEvent;

    protected string eventName;

    private void Awake()
    {
        playEvent = false;
    }

    public virtual void StartEvent()
    {
        //This method is meant to be overwritten
        Debug.Log(eventName + " has been activated.");

    }

    private void Update()
    {
        
    }
}
