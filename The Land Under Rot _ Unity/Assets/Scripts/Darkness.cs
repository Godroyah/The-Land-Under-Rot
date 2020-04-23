using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darkness : MonoBehaviour
{
    MeshRenderer mRenderer;
    Material darkMat;

    private GameObject baseModel;

    [Range(1, 5)]
    public int steps = 3;
    public float stepDepth = 2f;
    public Maintain direction = Maintain.NONE;
    public GameObject cameraBlinder;
    private MeshRenderer camMeshRenderer;

    public float illuminationDuration = 5f;
    public float transitionDuration = 1;
    private Coroutine newTransition;
    private Coroutine currentTransition;

    private List<DarknessHelper> helpers = new List<DarknessHelper>();

    void Start()
    {
        baseModel = transform.GetChild(0).gameObject;
        mRenderer = baseModel.GetComponent<MeshRenderer>();
        darkMat = mRenderer.sharedMaterial;
        darkMat = new Material(darkMat);
        mRenderer.sharedMaterial = darkMat;

        camMeshRenderer = cameraBlinder.GetComponent<MeshRenderer>();

        camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, 0));

        if (baseModel.transform.localScale != Vector3.one)
        {
            Debug.LogWarning("Darkness Volume's base has been scaled. Please only scale the parent");
            baseModel.transform.localScale = Vector3.one;
        }

        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        DarknessHelper tempHelp;

        for (int i = 0; i < steps; i++)
        {
            //GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject temp = Instantiate(baseModel);

            Vector3 maintainDirection = Vector3.zero;
            switch (direction)
            {
                case Maintain.NONE:
                    maintainDirection = Vector3.one;
                    break;
                case Maintain.X__:
                    maintainDirection = new Vector3(0, 1, 1);
                    break;
                case Maintain._Y_:
                    maintainDirection = new Vector3(1, 0, 1);
                    break;
                case Maintain.__Z:
                    maintainDirection = new Vector3(1, 1, 0);
                    break;
                case Maintain.XY_:
                    maintainDirection = new Vector3(0, 0, 1);
                    break;
                case Maintain.X_Z:
                    maintainDirection = new Vector3(0, 1, 0);
                    break;
                case Maintain._YZ:
                    maintainDirection = new Vector3(1, 0, 0);
                    break;
                case Maintain.XYZ:
                    maintainDirection = Vector3.zero;
                    break;
                default:
                    break;
            }

            temp.transform.localScale = this.transform.localScale - (maintainDirection * stepDepth * (i + 1));
            temp.transform.parent = this.transform;
            temp.transform.localPosition = Vector3.zero;

            temp.GetComponent<MeshRenderer>().sharedMaterial = darkMat;
            tempHelp = temp.AddComponent<DarknessHelper>();
            tempHelp.parentDarkness = this;
            tempHelp.SetDarknessLevel(i + 1);
            helpers.Add(tempHelp);
            yield return new WaitForEndOfFrame();
        }

        tempHelp = baseModel.AddComponent<DarknessHelper>();
        tempHelp.parentDarkness = this;
        tempHelp.SetDarknessLevel(0);
        helpers.Add(tempHelp);
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
        float originalValue = camMeshRenderer.material.GetColor("_BaseColor").a;
        //blinder.a = percent;
        //Debug.Log(blinder.a);
        //cameraBlinder.color = blinder;
        //UIBlinder.material = cameraBlinder;
        //yield return new WaitForEndOfFrame();

        while (time <= transitionDuration)
        {
            yield return new WaitForEndOfFrame();
            float newAlpha = Mathf.Lerp(originalValue, percent, time / transitionDuration);

            camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, newAlpha));
            time += Time.deltaTime;
        }

        camMeshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0, percent));
    }

    public void AdjustBlinder(float level)
    {
        if (level <= 0)
            level = 0.5f;

        float percent = level / (steps * 1f);

        percent = 0.5f + (percent * 0.5f);

        newTransition = StartCoroutine(LerpAlpha(percent));
    }

    public void RemoveBlinder()
    {
        newTransition = StartCoroutine(LerpAlpha(0));
    }

    public void Illuminate()
    {
        foreach (DarknessHelper helper in helpers)
        {
            helper.IlluminateDarkness(illuminationDuration);
        }
    }
}

public enum Maintain
{
    NONE,
    X__,
    _Y_,
    __Z,
    XY_,
    X_Z,
    _YZ,
    XYZ
}
