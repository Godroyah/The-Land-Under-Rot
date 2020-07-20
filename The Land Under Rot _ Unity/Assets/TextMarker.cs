using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMarker : MonoBehaviour
{
    public Transform characterPos;
    public Image thisImage;
    private Transform playerCam;
    public float distToPlayer;
    public GameObject player;
    public CanvasGroup imageAlpha;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(characterPos.transform.position, player.transform.position);

        //if (distToPlayer <= 20.0f)
        //{
        //    imageAlpha.alpha += 0.1f;
        //}
        //else
        //{
        //    imageAlpha.alpha -= 0.1f;
        //}

        float minX = thisImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = thisImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 tempPos = Camera.main.WorldToScreenPoint(characterPos.position);

        if (Vector3.Dot((characterPos.position - playerCam.position), playerCam.forward) < 0)
        {
            if (tempPos.x < Screen.width / 2)
            {
                tempPos.x = maxX;
            }
            else
            {
                tempPos.x = minX;
            }
        }

        tempPos.x = Mathf.Clamp(tempPos.x, minX, maxX);
        tempPos.y = Mathf.Clamp(tempPos.y, minY, maxY);

        thisImage.transform.position = tempPos;
    }
}
