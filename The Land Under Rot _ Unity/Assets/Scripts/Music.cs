using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour // Replace ClassName with whatever your class name is
{
    public static Music singleton; // ClassName here should match the class name above
    private void Start()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        AkSoundEngine.PostEvent("Start_Game", gameObject); // Call whatever event starts the music
    }
}
