using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public List<GameObject> Dialogue_GameObjects;

    Dialogue[] dialogues;

    public Canvas myCanvas;

    public NPC_Emotes[] npcs;

    public bool prepCamEvent;
    public bool hasActiveDialogue = false;

    [Header("Scene Testing")]
    public GameObject TestScene;
    public bool isTestingScene = false;

    [Range(1, 45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 25;

    [Header("Music Stuff")]
    public AudioSource dialogueMusicAS;
    public AudioClip dialogueMusic;
    public bool dialogueMusicIsPlaying;
    public bool shouldTurnOffMusic;

    private bool activeDialogueEvent;

    Dialogue dialogue;

    private void Awake()
    {
        myCanvas.worldCamera = Camera.main;

        //dialogues = new Dialogue[Dialogue_GameObjects.Count];

        //for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        //{
        //    dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
        //    dialogues[i].dialogueManager = this;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.dialogueManager = this;

        dialogues = new Dialogue[Dialogue_GameObjects.Count];

        for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        {
            dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
            dialogues[i].dialogueManager = this;
        }

        //for(int i = 0; i < npcs.Length; i++)
        //{
        //    npcs[i].emote = npcs[i].Emote.ToString();
        //}
        //  OR
        //StartCoroutine(ObtainCutsceneScripts());

        if (TestScene != null && isTestingScene)
        {
            StartDialogue(TestScene.GetComponent<Dialogue>().SceneName);
        }

        
    }

    private void LateUpdate()
    {
        //Debug.Log(battleManager.isInBattle);
        if (hasActiveDialogue == false && dialogueMusicIsPlaying == true)
        {
            shouldTurnOffMusic = true;

            if (shouldTurnOffMusic == true)
            {
                dialogueMusicAS.enabled = false;
                dialogueMusicIsPlaying = false;
                shouldTurnOffMusic = false;
            }
        }
    }

    IEnumerator ObtainDialogueScripts()
    {
        for (int i = 0; i < Dialogue_GameObjects.Count; i++)
        {
            dialogues[i] = Dialogue_GameObjects[i].GetComponent<Dialogue>();
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public bool StartDialogue(string sceneName)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                if (dialogue.SceneName == sceneName)
                {
                    dialogue.StartScene();
                    //Debug.Log(sceneName);
                    hasActiveDialogue = true;
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("Unable to start new scene '" + sceneName + "' because another scene is still/already started");
        }

        return false;
    }

    public bool StartDialogue(Reply sceneName)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                if (dialogue.SceneName == sceneName.ToString())
                {
                    dialogue.StartScene();
                    //Debug.Log(sceneName);
                    hasActiveDialogue = true;
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("Unable to start new scene '" + sceneName + "' because another scene is still/already started");
        }

        return false;
    }

    public bool StartDialogue(Dialogue dialogue)
    {
        //Debug.Log(sceneName);
        if (!hasActiveDialogue)
        {
            dialogue.StartScene();
        }
        else
        {
            Debug.Log("Unable to start new scene '" + dialogue.SceneName + "' because another scene is still/already started");
        }

        return false;
    }

}

[System.Serializable]
public class NPC_Emotes
{
    //public ManagerEmote Emote;
    //public string emote;
    public string NPC;
    public EmoteParticles npcEmotes;
}

//public enum ManagerEmote
//{
//    SLEEPING,
//    WAITING,
//    EXCLAMATION,
//    ANGRY,
//    SHOCKED,
//    CONFUSED,
//    DIZZY,
//    HAPPY
//}

public enum Reply
{
    #region Understump Dialogue
    Mulchant_Intro_US,
    Mulchant_No_Mulch_US,
    Mulchant_Gathered_Mulch_US,
    Mulchant_Tree_Awake_US,
    Willow_Tree_Asleep,
    Willow_Tree_Awake,
    Willow_Tree_Post_Cutscene,
    Willow_Tree_Repeat_1,
    Spud_Intro,
    Spud_Tree_Awake,
    Spud_Repeat_1,
    Yellow_GG_Fenway,
    #endregion

    #region FruitForest Dialogue
    Mulchant_Intro_FF,
    Mulchant_No_Mulch_FF,
    Mulchant_Gathered_Mulch_FF,
    Mulchant_Tree_Awake_FF,
    Star_Tree_Sleeping,
    Star_Tree_Awake,
    Star_Tree_Worms_Gone,
    Star_Tree_Repeat_1,
    Star_Tree_Repeat_2,
    Little_Blue_Intro,
    Little_Blue_Tree_Awake,
    Little_Blue_Repeat_1,
    Little_Blue_Repeat_2,
    Banan_Intro,
    Banan_Tree_Awake,
    Banan_Repeat_1,
    Banan_Repeat_2,
    Strawbert_Intro,
    Strawbert_Tree_Awake,
    Strawbert_Repeat_1,
    Strawbert_Repeat_2,
    One_Way_Bark_Fenway,
    Green_GG_Fenway,
    #endregion

    #region TreeSeat Dialogue
    //TreeSeat
    // - Key_Dialogue
    Sleepy_AngelTree,
    Awake_AngelTree,
    AngelTree_PostCutscene,
    Mulchant_Intro,
    Mulchant_No_Mulch,
    Mulchant_Gathered_Mulch_PreCutscene,
    Mulchant_Tree_Is_Awake,
    Return_To_Stinkhorn,

    // - Other Dialogue
    TS_Rootford_Intro,
    TS_Rootford_Repeat,
    Catkin_Intro,
    CK_Spoke_To_Mulchant,
    CK_Tree_Is_Awake,
    Catkin_Repeat_1,
    Catkin_Repeat_2,
    Catkin_Repeat_3,
    Catkin_Repeat_4,
    Pedalton,
    Carrot_Slug_Intro,
    Carrot_Slug_Repeat_1,
    Carrot_Slug_Repeat_2,
    Carrot_Slug_Repeat_3,
    Carrot_Slug_Repeat_4,
    Stranger_Intro,
    Stranger_Tree_Is_Awake,
    Stranger_Repeat_1,
    Stranger_Repeat_2,
    Stranger_Repeat_3,
    Miss_Stamen_Intro,
    Miss_Stamen_Spoke_To_Mulchant,
    Miss_Stamen_Tree_Is_Awake,
    Miss_Stamen_Repeat_1,
    Miss_Stamen_Repeat_2,
    Buddy,
    TS_Fenway_Exit,
    TS_Fenway_Mulch,
    Gourdo_Intro,
    Gourdo_Tree_Is_Awake,
    Gourdo_Repeat_1,
    Gourdo_Repeat_2,
    Gourdo_Repeat_3,
    Lizard_1,
    Lizard_2,
    Lizard_3,
    Lizard_4,
    #endregion

    #region Stinkhorn Dialogue
    //Stinkhorn_Stop
    SS_Rootford_Intro_1,
    SS_Rootford_Intro_2,
    SS_Rootford_Intro_3_Repeat,
    SS_Rootford_Bus_1_Repeat,
    SS_BusDriver_1,
    SS_BusDriver_2_Repeat,
    MrPots_Sign,
    Stink_Fenway_Intro,
    Stink_Fenway_Repeat,
    Red_GG_Fenway,
    Secrets_Fenway,
    Test,
    #endregion

    #region Tutorial_Area Dialogue
    //Tutorial_Area
    Tut_Fenway_Movement,
    Tut_Fenway_Headbutt,
    Tut_Fenway_Jump,
    Tut_Blue_GG_Fenway
    #endregion
}