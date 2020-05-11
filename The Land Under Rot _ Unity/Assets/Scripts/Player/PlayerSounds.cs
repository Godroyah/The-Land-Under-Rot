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
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Purple Mushroom")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Henlo");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Mushroom")
        {
            surfaceType = WalkSurface.STONE;
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
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