using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSFX : MonoBehaviour
{
    public ContactType contactType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.Instance.Play_OnContact(contactType);
        }
    }
}
