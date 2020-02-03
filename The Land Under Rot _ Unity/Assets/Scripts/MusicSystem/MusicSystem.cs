using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    private AudioSource oldSource;
    private AudioSource currentSource;

    public AudioSource newSource;

    private Coroutine transitioning;

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
            StopCoroutine(transitioning);
            transitioning = StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        oldSource = currentSource;
        currentSource = newSource;
        newSource = null;
        float sourceCurrent_MaxVolume = currentSource.volume;
        newSource.volume = 0;

        // Ramps Down the old source
        while (oldSource.volume > 0)
        {
            float check = oldSource.volume - transitionSpeed * Time.deltaTime;
            if (check < 0f)
            {
                oldSource.volume = 0;
                break;
            }
            oldSource.volume -= transitionSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Ramps up the new (current) source
        while (currentSource.volume < sourceCurrent_MaxVolume)
        {
            float check = currentSource.volume + transitionSpeed * Time.deltaTime;
            if (check > sourceCurrent_MaxVolume)
            {
                newSource.volume = sourceCurrent_MaxVolume;
                break;
            }
            currentSource.volume += transitionSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MusicTrigger"))
        {
           AudioSource tempSource = other.GetComponent<MusicTrigger>().audioSource;
            if (tempSource == currentSource || tempSource == newSource)
            {
                Debug.Log("MusicSystem entered the same trigger as the current source");
            }
            else
                newSource = tempSource;
        }
    }
}
