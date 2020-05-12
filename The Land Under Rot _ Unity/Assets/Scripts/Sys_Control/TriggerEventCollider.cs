using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventCollider : MonoBehaviour
{
  //  public AK.Wwise.Event OnTriggerEnterEvent;
 //   public AK.Wwise.Event OnTriggerExitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent("Start_Mulchant_Theme", gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent("Stop_Mulchant_Theme", gameObject);
        }
    }
}
