using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Type : MonoBehaviour
{
    protected bool playEvent;

    [SerializeField]
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


}
