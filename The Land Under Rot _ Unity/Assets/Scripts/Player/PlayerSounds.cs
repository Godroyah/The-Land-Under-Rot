using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    //  public bool soundplayed;
    public WalkSurface surfaceType = WalkSurface.NONE;
    public GameObject player;
    private bool isCrossfading;
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    public void footstepPlay()
    {
            AudioManager.Instance.Play_Walk(surfaceType);
        Debug.Log("I FOOTSTEP WORK HA");
    }
    /* public void footstepStop()
     {
         AkSoundEngine.PostEvent("stop", gameObject);
     }
 */
    // Update is called once per frame
    void Update()
    {

         

    }
}