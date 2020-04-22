using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurfaceType { NONE, WOOD, STONE }

public class HeadbuttSFX : MonoBehaviour
{
    public SurfaceType surface;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            switch(surface)
            {
                case SurfaceType.NONE:
                    Debug.Log("Shush");
                    break;
                case SurfaceType.WOOD:
                    AudioManager.Instance.Play_Headbutt_Wood();
                    break;
                case SurfaceType.STONE:
                    AudioManager.Instance.Play_Headbutt_Stone();
                    break;
            }
        }
    }
}
