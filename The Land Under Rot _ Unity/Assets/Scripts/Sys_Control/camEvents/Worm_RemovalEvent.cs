using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm_RemovalEvent : Event_Type
{
    public GameObject[] worms;

    public Vector3[] startPos;
    public Vector3[] loweredPos;

    public float lowerDist;

    float[] dropSpeed = new float[] { 0, 0, 0, 0, 0, 0 };

    float[] totalSpeed = new float[] { 0, 0, 0, 0, 0, 0 };

    float[] dropRecord = new float[] { 0, 0, 0, 0, 0, 0 };

    float[] iteration = new float[] { 0, 0, 0, 0, 0, 0 };

    [Range(1.0f, 15.0f)]
    public float animationDelay;

    // Start is called before the first frame update
    void Start()
    {


        for(int i = 0; i < worms.Length; i++)
        {
            dropSpeed[i] = 0.15f;
            totalSpeed[i] = 0.55f;
            dropRecord[i] = dropSpeed[i];

            startPos[i] = new Vector3(worms[i].transform.position.x, worms[i].transform.position.y, worms[i].transform.position.z);
            loweredPos[i] = new Vector3(worms[i].transform.position.x, worms[i].transform.position.y - lowerDist, worms[i].transform.position.z);
        }


    }

    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(RemoveWorms());
       
    }

    IEnumerator RemoveWorms()
    {
        yield return new WaitForSeconds(animationDelay);

        for (int i = 0; i < worms.Length; i++)
        {
            dropSpeed[i] = dropRecord[i];
            iteration[i] = dropSpeed[i];
        //}
        //for (int i = 0; i < worms.Length; i++)
        //{
            while (dropSpeed[i] < totalSpeed[i])
            {
                worms[i].transform.position = Vector3.Lerp(worms[i].transform.position, loweredPos[i], dropSpeed[i] * Time.deltaTime);
                dropSpeed[i] += iteration[i] * Time.deltaTime;
                yield return null;
            }
        }
    }
}
