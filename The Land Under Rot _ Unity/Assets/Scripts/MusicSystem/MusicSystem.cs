using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    private AudioSource currentSource;

    public AudioSource newSource;

    [Range(0.01f, 1f)]
    public float transitionSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (newSource != null)
        {
            StartCoroutine(Transition(currentSource, newSource));
            newSource = null;
        }
    }

    IEnumerator Transition(AudioSource source1, AudioSource source2)
    {

        float source2_MaxVolume = source2.volume;
        source2.volume = 0;

        while (source1.volume > 0)
        {
            float check = source1.volume - transitionSpeed * Time.deltaTime;
            if (check < 0f)
            {
                source1.volume = 0;
                break;
            }
            source1.volume -= transitionSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (source2.volume < source2_MaxVolume)
        {
            float check = source1.volume + transitionSpeed * Time.deltaTime;
            if (check > source2_MaxVolume)
            {
                source2.volume = source2_MaxVolume;
                break;
            }
            source2.volume += transitionSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MusicTrigger"))
        {
            newSource = other.GetComponent<MusicTrigger>().audioSource;
        }
    }
}
