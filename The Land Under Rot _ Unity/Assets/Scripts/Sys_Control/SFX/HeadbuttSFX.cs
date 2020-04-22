using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurfaceType { NONE, WOOD, STONE, GAZEGROWTH, MULCH}

public class HeadbuttSFX : MonoBehaviour
{
    public SurfaceType hitType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            switch(hitType)
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
                case SurfaceType.GAZEGROWTH:
                    AudioManager.Instance.Play_EyeBoing();
                    break;
                case SurfaceType.MULCH:
                    AudioManager.Instance.Play_SmashingMulch();
                    break;
            }
        }
    }
}
