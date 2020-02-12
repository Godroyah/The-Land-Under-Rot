using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeGrowth : Interactable
{
    public GazeGrowthType growthType;

    public GameObject[] cordyseps;

    //public Transform[] cordys_Start_Position;

    private Vector3 cordys_Start_Position;

    private Vector3 cordys_Lowered_Position;

    public float lowerDistance;

    public float moveDuration;

    private float currentDuration;

    //private Vector3 cordyRaisedVector;

    //private Vector3 cordyLoweredVector;

    //[Range(0f, 1f), Tooltip("Set to change how quickly cordyseps transition.")]
    //public 
    public float moveSmoothing;
    
    private float rateOfChange;

    [Range(0f, 25f), Tooltip("If set to 0, the cordyseps will never return.")]
    public float returnIn = 0f;

    GameController gameController;

    private void Start()
    {
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

        switch (growthType)
        {
            // TODO: Needs proper movement/animation
            case GazeGrowthType.White:
                StartCoroutine(Fungi(0));
                break;
            case GazeGrowthType.Red:
                StartCoroutine(Fungi(returnIn));
                break;
            default:
                break;
        }
    }

    IEnumerator Fungi(float waitTime)
    {
        

        foreach (GameObject fungi in cordyseps)
        {
            currentDuration = 0;
            rateOfChange = 0;
            Debug.Log("Shrinking!");

            cordys_Start_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y, fungi.transform.position.z);
            cordys_Lowered_Position = new Vector3(fungi.transform.position.x, fungi.transform.position.y - lowerDistance, fungi.transform.position.z);

            while(currentDuration < moveDuration)
            {
                //rateOfChange = Mathf.InverseLerp(0, moveDuration, currentDuration);
                rateOfChange = (currentDuration / moveDuration) * ;
                fungi.transform.position = Vector3.Lerp(cordys_Start_Position, cordys_Lowered_Position, rateOfChange);
                currentDuration += Time.deltaTime;
            }
            //transform.position = transform.position - (Vector3.down * 20f);
        }

        if (waitTime != 0)
        {
            yield return new WaitForSeconds(waitTime);

            foreach (GameObject fungi in cordyseps)
            {
                currentDuration = 0;
                rateOfChange = 0;

                while (currentDuration < moveDuration)
                {
                    //rateOfChange = Mathf.InverseLerp(0, moveDuration, currentDuration);
                    rateOfChange = currentDuration / moveDuration;
                    fungi.transform.position = Vector3.Lerp(cordys_Start_Position, cordys_Lowered_Position, rateOfChange);
                    currentDuration += Time.deltaTime;
                }
                //transform.position = transform.position + (Vector3.up * 20f);
            }

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
