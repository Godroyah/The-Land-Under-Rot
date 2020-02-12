using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeGrowth : Interactable
{
    public GazeGrowthType growthType;

    //public GameObject[] cordyseps;

    public GameObject cordysepBarrier;

    //public Transform[] cordys_Start_Position;

    private Vector3 cordys_Start_Position;

    private Vector3 cordys_Lowered_Position;

    private float dropDistance;

    public float lowerDistance;

    //public float moveDuration;

    //[SerializeField]
    //private float currentDuration;

    //[SerializeField]
    //private float startTime;

    //public bool activated;
    //public bool returning;
    //private Vector3 cordyRaisedVector;

    //private Vector3 cordyLoweredVector;

    //[Range(0f, 1f), Tooltip("Set to change how quickly cordyseps transition.")]
    //public 
    private float rateStorage;

    [SerializeField]
    private float rateOfChange = 0.1f;

    [Range(0f, 25f), Tooltip("If set to 0, the cordyseps will never return.")]
    public float returnIn = 0f;

    GameController gameController;

    private void Start()
    {
        rateStorage = rateOfChange;
        cordys_Start_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y, cordysepBarrier.transform.position.z);
        cordys_Lowered_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y - lowerDistance, cordysepBarrier.transform.position.z);

        #region GameController Search
        GameObject temp = GameObject.Find("@GameController");
        if (temp != null)
        {
            gameController = temp.GetComponent<GameController>();

            if (gameController == null)
                Debug.LogWarning("@GameController does not have the 'GameController' script!");
        }
        else
            Debug.LogWarning("Could not find GameController.");

        #endregion

        //for(int i = 0; i < cordyseps.Length; i++)
        //{
        //    cordys_Start_Position[i].position = cordyseps[i].transform.position;
        //}


        //cordyRaisedVector = new Vector3(cordys_Raised_Position.position.x, cordys_Raised_Position.position.y, cordys_Raised_Position.position.z);
        //cordyLoweredVector = new Vector3(cordys_Lowered_Position.position.x, cordys_Lowered_Position.position.y, cordys_Lowered_Position.position.z);
    }

    public override void Interact()
    {
        base.Interact();

        //startTime = Time.time;
        //activated = true;

        switch (growthType)
        {
            // TODO: Needs proper movement/animation
            case GazeGrowthType.White:
                StartCoroutine(Fungi(0));
                break;
            case GazeGrowthType.Red:
                StartCoroutine(Fungi(returnIn));
                //returning = true;
                break;
            default:
                break;
        }
    }

    //private void Update()
    //{
    //    if(activated)
    //    DropGate();
    //}

    //void DropGate()
    //{
    //    foreach (GameObject fungi in cordyseps)
    //    {

    //    }
    //}

    IEnumerator Fungi(float waitTime)
    {
        //cordys_Start_Position = new Vector3[cordyseps.Length];
        //cordys_Lowered_Position = new Vector3[cordyseps.Length];
        rateOfChange = rateStorage;
        float iteration = rateOfChange;
        //int i = 0;
        //for(int i = 0; i > cordyseps.Length; i++)
        //{
        //    cordys_Start_Position[i] = new Vector3(cordyseps[i].transform.position.x, cordyseps[i].transform.position.y, cordyseps[i].transform.position.z);
        //    cordys_Lowered_Position[i] = new Vector3(cordyseps[i].transform.position.x, cordyseps[i].transform.position.y - lowerDistance, cordyseps[i].transform.position.z);

        //    while(Vector3.Distance(cordyseps[i].transform.position, cordys_Lowered_Position[i]) > 0.1)
        //    {
        //        cordyseps[i].transform.position = Vector3.Lerp(cordys_Start_Position[i], cordys_Lowered_Position[i], rateOfChange);
        //        rateOfChange += iteration;
        //        yield return null;
        //    }
        //}
        //cordys_Start_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y, cordysepBarrier.transform.position.z);
        //cordys_Lowered_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y - lowerDistance, cordysepBarrier.transform.position.z);

        while (rateOfChange < 1.0f)
        {
            cordysepBarrier.transform.position = Vector3.Lerp(cordysepBarrier.transform.position, cordys_Lowered_Position, rateOfChange);
            rateOfChange += iteration;
            yield return null;
        }

        if(waitTime != 0)
        {
            StartCoroutine(DelayReturn(waitTime));
        }
        //foreach (GameObject fungi in cordyseps)
        //{
        //    cordys_Start_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y, fungi.transform.position.z);
        //    cordys_Lowered_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y - lowerDistance, fungi.transform.position.z);

        //    //while (Vector3.Distance(fungi.transform.position, cordys_Lowered_Position) > 0.1)
        //    while(rateOfChange < 1.0f)
        //    {
        //        fungi.transform.position = Vector3.Lerp(cordys_Start_Position, cordys_Lowered_Position, rateOfChange);
        //        rateOfChange += iteration;
        //        yield return null;
        //    }
        //}
    }

    IEnumerator DelayReturn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(ReturnVines());
    }

    IEnumerator ReturnVines()
    {
        rateOfChange = rateStorage;
        float iteration = rateOfChange;
        //int i = 0;
        //for(int i = 0; i > cordyseps.Length; i++)
        //{
        //    cordys_Start_Position[i] = new Vector3(cordyseps[i].transform.position.x, cordyseps[i].transform.position.y, cordyseps[i].transform.position.z);
        //    cordys_Lowered_Position[i] = new Vector3(cordyseps[i].transform.position.x, cordyseps[i].transform.position.y - lowerDistance, cordyseps[i].transform.position.z);

        //    while(Vector3.Distance(cordyseps[i].transform.position, cordys_Lowered_Position[i]) > 0.1)
        //    {
        //        cordyseps[i].transform.position = Vector3.Lerp(cordys_Start_Position[i], cordys_Lowered_Position[i], rateOfChange);
        //        rateOfChange += iteration;
        //        yield return null;
        //    }
        //}
        //cordys_Start_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y, cordysepBarrier.transform.position.z);
        //cordys_Lowered_Position = new Vector3(cordysepBarrier.transform.position.x, cordysepBarrier.transform.position.y - lowerDistance, cordysepBarrier.transform.position.z);

        while (rateOfChange < 1.0f)
        {
            cordysepBarrier.transform.position = Vector3.Lerp(cordysepBarrier.transform.position, cordys_Start_Position, rateOfChange);
            rateOfChange += iteration;
            yield return null;
        }
    }

    IEnumerator Fungu(float waitTime)
    {


        //foreach (GameObject fungi in cordyseps)
        //{
        //    currentDuration = 0;
        //    rateOfChange = 0;
        //    Debug.Log("Shrinking!");

        //    cordys_Start_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y, fungi.transform.position.z);
        //    cordys_Lowered_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y - lowerDistance, fungi.transform.position.z);

        //    dropDistance = Vector3.Distance(cordys_Start_Position, cordys_Lowered_Position);

        //    while (Vector3.Distance(fungi.transform.position, cordys_Lowered_Position) > 0.1f)
        //    {
        //        rateOfChange = Mathf.InverseLerp(0, moveDuration, currentDuration);
        //        rateOfChange = (currentDuration / moveDuration);
        //        float distCovered = (Time.time - startTime) * moveSmoothing;
        //        rateOfChange = distCovered / dropDistance;
        //        fungi.transform.position = Vector3.Lerp(cordys_Start_Position, cordys_Lowered_Position, rateOfChange);
        //        currentDuration += Time.deltaTime;
        //    }
        //    transform.position = transform.position - (Vector3.down * 20f);
        //}

        if (waitTime != 0)
        {
            yield return new WaitForSeconds(waitTime);



            //foreach (GameObject fungi in cordyseps)
            //{
            //    currentDuration = 0;
            //    rateOfChange = 0;

            //    while (Vector3.Distance(fungi.transform.position, cordys_Start_Position) > 0.1f)
            //    {

            //        float distCovered = (Time.time - startTime) * moveSmoothing;
            //        rateOfChange = distCovered / dropDistance;
            //        fungi.transform.position = Vector3.Lerp(cordys_Start_Position, cordys_Lowered_Position, rateOfChange);

            //    }

            //}

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headbutt"))
        {
            Interact();
            //gameController.playerController.headbuttables.Add(this);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Headbutt"))
    //    {
    //        gameController.playerController.headbuttables.Remove(this);
    //    }
    //}
}

public enum GazeGrowthType
{
    White, Red
}
