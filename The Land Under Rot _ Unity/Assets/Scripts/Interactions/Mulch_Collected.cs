using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mulch_Collected : MonoBehaviour
{
    public TextMeshProUGUI mulch_text;

    private float fadeSpeed = 15.0f;
    //private bool useThisText;
    //private bool useTextText;
    private string thisText;
    //private int textLength;
    private bool fade;

    private float colorFloat = 0.1f;
    private int colorInt;
    private int letterCounter = 0;
    private string shownText;
    private string currentCharacter;

    private void Start()
    {
        //textToUse is the text component or mulch_text
        //textToShow is mulchtext.text
        thisText = mulch_text.text;
        //textLength = mulch_text.text.Length;
        Debug.Log("Renamed");

        StartCoroutine(AnnounceMulch());
    }

    //public void GotMulch()
    //{
    //    StartCoroutine(AnnounceMulch());
    //}


    

    IEnumerator AnnounceMulch()
    {
        //float startAlpha = mulch_text.color.a;
        //float progress = 0.0f;
        //currentCharacter = mulch_text.text[letterCounter].ToString();

       // Debug.Log("Current character is: " + currentCharacter);

        while (letterCounter < thisText.Length)
        {
            //currentCharacter = mulch_text.text[letterCounter].ToString();
            //Debug.Log("Announced?");
            if (colorFloat < 1.0f)
            {
                colorFloat += Time.deltaTime * fadeSpeed;
                colorInt = (int)(Mathf.Lerp(0.0f, 1.0f, colorFloat) * 255.0f);
                //thisText[letterCounter].

                mulch_text.text = shownText + string.Format(thisText[letterCounter].ToString(), colorInt);

                //mulch_text.text = shownText + "<color=\"#FFFFFF" + string.Format("{0:X}", colorInt) + "\">"
                    //+ thisText[letterCounter] + "</color>";
                    //+ thisText[letterCounter];

                //mulch_text.text = shownText +  mulch_text.text[letterCounter];
                //Debug.Log(colorInt);


                //Debug.Log("Spelling");
            }
            else
            {
                colorFloat = 0.1f;
                //currentCharacter = mulch_text.text[letterCounter].ToString();
                shownText += mulch_text.text[letterCounter];
                letterCounter++;
                //Debug.Log("Count Up");
            }
            yield return null;
        }

        yield return new WaitForSeconds(3.0f);

        DestroyThis();
    }

    //^
    //Code referenced from TheFlyingKeyBoard
    //http://theflyingkeyboard.net/unity/unity-ui-c-fade-in-text-letter-by-letter/

    //// ? 2017 TheFlyingKeyboard and released under MIT License
    // theflyingkeyboard.net
    //Accessed 4/14/2020
    //Not really working as advertised/intended


    //public void SetName(string text)
    //{
    //    mulch_text.text = text;
    //    textLength = mulch_text.text.Length;
    //    Debug.Log("Renamed");
    //}

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
