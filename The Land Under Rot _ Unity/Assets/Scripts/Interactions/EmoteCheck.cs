using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmoteType { NONE, SLEEPING, WAITING, EXCLAMATION, ANGRY, SHOCKED, CONFUSED, DIZZY, HAPPY }

public class EmoteCheck : MonoBehaviour
{
    //public DialogueManager dialogueManager;
    public Dialogue dialogue;

    //[Header("Use the Drop Box, leave the text box BLANK!")]
    public EmoteType Emote;
    //public string emote;

    [Space(5)]

    [Header("'PLAY' IS NOT A TOY! LEAVE UNCHECKED!")]
    [HideInInspector]
    public bool play = false;
   
    //public bool ready = true;

    // Start is called before the first frame update
    //void Start()
    //{
    //    ready = false;
    //    emote = Emote.ToString();
    //}

    private void OnEnable()
    {
        if(Emote != EmoteType.NONE)
        {
            play = true;
        }

        //if(Emote != EmoteType.NONE)
        //play = true;
        //if(!ready)
        //{

        //}
    }

    private void OnDisable()
    {
        play = false;
    }
}
