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
    public Image UIBlinder;
    public Material cameraBlinder;
    private Color blinder;
    public float transitionDuration = 1;
    private Coroutine newTransition;
    private Coroutine currentTransition;

    private List<GameObject> children = new List<GameObject>();

    void Start()
    {
        baseModel = transform.GetChild(0).gameObject;
        mRenderer = baseModel.GetComponent<MeshRenderer>();
        darkMat = mRenderer.sharedMaterial;
        darkMat = new Material(darkMat);
        mRenderer.sharedMaterial = darkMat;

        cameraBlinder = UIBlinder.material;
        blinder = cameraBlinder.color;
        cameraBlinder = new Material(cameraBlinder);

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
            children.Add(temp);
            yield return new WaitForEndOfFrame();
        }

        tempHelp = baseModel.AddComponent<DarknessHelper>();
        tempHelp.parentDarkness = this;
        tempHelp.SetDarknessLevel(0);
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
        float originalValue = blinder.a;
        //blinder.a = percent;
        //Debug.Log(blinder.a);
        //cameraBlinder.color = blinder;
        //UIBlinder.material = cameraBlinder;
        //yield return new WaitForEndOfFrame();
        
        while (time <= transitionDuration)
        {
            yield return new WaitForEndOfFrame();
            blinder.a = Mathf.Lerp(originalValue, percent, time/transitionDuration);
            Debug.Log(blinder.a);
            cameraBlinder.color = blinder;
            UIBlinder.material = cameraBlinder;
            time += Time.deltaTime;
        }
    }

    public void AdjustBlinder(int level)
    {
        if (level < 0)
            level = 0;

        float percent = level / (steps * 1f);

        newTransition = StartCoroutine(LerpAlpha(percent));
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
