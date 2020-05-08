using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darkness : MonoBehaviour
{
    MeshRenderer mRenderer;
    Material darkMat;
    private CamControl camController;

    private GameObject baseModel;

    [Range(1, 5), Tooltip("The number of additional layers to use")]
    public int steps = 3;
    [Tooltip("The distance before the next additional layer")]
    public float stepDepth = 2f;
    [Tooltip("This is used to 'maintain' the specific axis of the additional layers and not create the illusion of depth.")]
    public Maintain direction = Maintain.NONE;

    private int currentLevel = -1;

    public float illuminationDuration = 5f;
    public float transitionDuration = 5f;

    private Coroutine newIllumination;
    private Coroutine currentIllumination;

    private List<DarknessHelper> helpers = new List<DarknessHelper>();
    public GameObject killVolume;

    void Start()
    {
        Transform temp = transform.GetChild(0);
        if (temp != null)
            baseModel = temp.gameObject;
        else
            Debug.LogWarning("This DarkVolume has no base model as a child!");
        mRenderer = baseModel.GetComponent<MeshRenderer>();
        darkMat = mRenderer.sharedMaterial;
        darkMat = new Material(darkMat);
        mRenderer.sharedMaterial = darkMat;

        if (baseModel.transform.localScale != Vector3.one)
        {
            Debug.LogWarning("Darkness Volume's base has been scaled. Please only scale the parent");
            baseModel.transform.localScale = Vector3.one;
        }

        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.1f);
        camController = GameController.Instance.playerController.camControl;

        DarknessHelper tempHelp;

        for (int i = 0; i < steps; i++)
        {
            //GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject temp = Instantiate(baseModel, this.transform);

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

            temp.transform.parent = null;
            temp.transform.localScale = baseModel.transform.lossyScale - (maintainDirection * stepDepth * (i + 1));
            temp.transform.parent = this.transform;
            /*
            temp.transform.localScale = baseModel.transform.lossyScale - (maintainDirection * stepDepth * (i + 1));
            temp.transform.parent = this.transform;
            temp.transform.localPosition = Vector3.zero;
            //temp.transform.localRotation = this.transform.localRotation;
            temp.transform.localRotation = baseModel.transform.localRotation;
            */

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

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void AdjustBlinder(float level)
    {
        currentLevel = Mathf.RoundToInt(level);

        if (level <= 0)
            level = 0.5f;

        float percent = level / (steps * 1f);

        percent = 0.5f + (percent * 0.5f);

        //newTransition = StartCoroutine(LerpAlpha(percent));
        camController.AdjustBlinder(percent);
    }

    public void RemoveBlinder()
    {
        currentLevel = -1;
        camController.RemoveBlinder();
    }

    public void Illuminate()
    {
        foreach (DarknessHelper helper in helpers)
        {
            helper.IlluminateDarkness(illuminationDuration);
            if (killVolume != null)
            {
                newIllumination = StartCoroutine(Illumination(illuminationDuration));
            }
        }
    }

    public void Illuminate(float duration)
    {
        foreach (DarknessHelper helper in helpers)
        {
            helper.IlluminateDarkness(duration);
            if (killVolume != null)
            {
                newIllumination = StartCoroutine(Illumination(duration));
            }
        }
    }

    IEnumerator Illumination(float duration)
    {
        if (currentIllumination != null)
        {
            StopCoroutine(currentIllumination);
        }
        currentIllumination = newIllumination;

        killVolume.SetActive(false);
        //if(!0)
        if (duration > 0)
        {
            yield return new WaitForSeconds(duration);
            killVolume.SetActive(true);
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
