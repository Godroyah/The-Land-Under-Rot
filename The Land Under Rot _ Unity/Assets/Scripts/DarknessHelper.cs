using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessHelper : MonoBehaviour
{
    public Darkness parentDarkness;
    private int darknessLevel;
    private float alphaLevel;
    private MeshRenderer meshRenderer;
    private Collider collider;

    public float transitionDuration = 1;
    private Coroutine newTransition;
    private Coroutine currentTransition;

    private Coroutine newIllumination;
    private Coroutine currentIllumination;

    public void SetDarknessLevel(int level)
    {
        darknessLevel = level;
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        alphaLevel = meshRenderer.material.GetColor("_BaseColor").a;
    }

    public void ApplyDarkness()
    {
        Debug.Log("Entered darkness: " + darknessLevel);
        parentDarkness.AdjustBlinder(darknessLevel);
        transitionDuration = parentDarkness.transitionDuration;

        //newTransition = StartCoroutine(LerpAlpha(0));
        AdjustAlpha(0);
    }

    public void RemoveDarkness()
    {
        Debug.Log("leaving darkness: " + darknessLevel);
        if (darknessLevel == 0)
            parentDarkness.RemoveBlinder();
        else
            parentDarkness.AdjustBlinder(darknessLevel - 1);
        //newTransition = StartCoroutine(LerpAlpha(alphaLevel));
        AdjustAlpha(alphaLevel);
    }

    public void AdjustAlpha(float percent)
    {
        newTransition = StartCoroutine(LerpAlpha(percent));
    }


    public void IlluminateDarkness(float duration)
    {
        newIllumination = StartCoroutine(Illumination(duration));
    }

    IEnumerator LerpAlpha(float percent)
    {
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }
        currentTransition = newTransition;

        Debug.Log("Lerping alpha");

        float time = 0;
        float originalValue = meshRenderer.material.GetColor("_BaseColor").a;

        while (time <= transitionDuration)
        {
            yield return new WaitForEndOfFrame();
            float newAlpha = Mathf.Lerp(originalValue, percent, time / transitionDuration);

            meshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, newAlpha));
            time += Time.deltaTime;
        }

        meshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, percent));
    }

    IEnumerator Illumination(float duration)
    {
        if (currentIllumination != null)
        {
            StopCoroutine(currentIllumination);
        }
        currentIllumination = newIllumination;

        collider.enabled = false;
        AdjustAlpha(0);

        if(duration > 0)
        {
            yield return new WaitForSeconds(duration);
            AdjustAlpha(alphaLevel);
            collider.enabled = true;
        }
    }
}
