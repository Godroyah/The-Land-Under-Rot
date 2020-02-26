using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource; // TODO: Music Trigger Prefab

    private void Awake()
    {
        if (audioSource == null)
        {
            AudioSource tempSource = gameObject.GetComponent<AudioSource>();

            if (tempSource == null)
                Debug.LogWarning("MusicTrigger is missing an audiosource.");
            else
                tempSource.mute = true;
        }
        else
            audioSource.mute = true;


    }
}
