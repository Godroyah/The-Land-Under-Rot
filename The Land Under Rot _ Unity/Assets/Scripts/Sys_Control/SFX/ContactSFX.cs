using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContactType { MUSHROOM_BOUNCE, WATER_SPLASH, LEAF_PILE}

public class ContactSFX : MonoBehaviour
{
    public ContactType contactType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(contactType)
            {
                case ContactType.MUSHROOM_BOUNCE:
                    AudioManager.Instance.Play_MushroomBounce();
                    break;
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
