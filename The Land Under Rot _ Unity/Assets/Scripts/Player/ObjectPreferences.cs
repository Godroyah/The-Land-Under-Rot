using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPreferences : MonoBehaviour
{
    public GameObject headbutt_ParticleEffect_player;
    public GameObject headbutt_ParticleEffect_obj;

    [Space(5)]

    public AudioClip headbutt_AudioClip;

    [Space(8)]

    public GameObject walk_ParticleEffect;
    public AudioClip walk_AudioClip;

    [Space(8)]

    public GameObject interact_ParticleEffect;
    public AudioClip interact_AudioClip;

    [Space(8)]
    public GameObject pickup_ParticleEffect;
    public AudioClip pickup_AudioClip;
}
