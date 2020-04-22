using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContactType { WATER_SPLASH, LEAF_PILE}

public class ContactSFX : MonoBehaviour
{
    public ContactType contactType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(contactType)
            {
                case ContactType.WATER_SPLASH:
                    AudioManager.Instance.Play_WaterSplash();
                    break;
                case ContactType.LEAF_PILE:
                    AudioManager.Instance.Play_LeafPile();
                    break;
            }
        }
    }
}
