using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Programming

    private static AudioManager _instance = null;
    public static AudioManager Instance { get { return _instance; } }
  

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    // These "folders" can be opened and closed using the plus/minus
    // symbol to the left of the grayed out words

    #region Character
    public void Play_Walk_Grass()
    {
        AkSoundEngine.PostEvent("char_footsteps_grass", gameObject);
    }

    public void Play_Walk_Stone()
    {
        AkSoundEngine.PostEvent("char_footsteps_stone", gameObject);
    }

    public void Play_Walk_Dirt()
    {
        AkSoundEngine.PostEvent("char_footsteps_dirt", gameObject);
    }

    public void Play_Walk_Mud()
    {
        AkSoundEngine.PostEvent("char_footsteps_grass", gameObject);
    }

    public void Play_Walk_Wood()
    {
        AkSoundEngine.PostEvent("char_footsteps_wood", gameObject);
    }

    public void Play_Walk_Mushroom()
    {

    }

    public void Play_Walk_Leaf()
    {

    }

    public void Play_Run()
    {

    }

    public void Play_Headbutt()
    {
        AkSoundEngine.PostEvent("char_headbutt", gameObject);
    }

    public void Play_Headbutt_Wood()
    {
        AkSoundEngine.PostEvent("char_headbutt_wood", gameObject);
    }

    public void Play_Headbutt_Stone()
    {
        AkSoundEngine.PostEvent("char_headbutt_stone", gameObject);
    }

    public void Play_Headbutt_Enemy()
    {

    }

    public void Play_Headbutt_EnemyRecoil()
    {
        AkSoundEngine.PostEvent("int_boing", gameObject);
    }

    public void Play_Falling()
    {
     //   AkSoundEngine.PostEvent("char_falling", gameObject);
    }

    public void Play_Landing_OnFeet()
    {

    }

    public void Play_Jump()
    {
        AkSoundEngine.PostEvent("char_jump", gameObject);
    }

    public void Play_Landing_OnButt()
    {

    }

    public void Play_ClothesRustle()
    {

    }
    #endregion

    #region Environment
    //distance
    public void Play_Ambient_BugsAroundLight()
    {

    }

    //unless these are specific bugs, no distance needed
    public void Play_Ambient_Bugs()
    {

    }

    //unless this is specific water in the location, no distance needed
    public void Play_Ambient_RunningWater()
    {

    }

    //distance needed if there are specific birds in the scene 
    public void Play_Ambient_BirdsChirping()
    {

    }

    //same as last ones, depends if theres specific water 
    public void Play_Ambience_Water()
    {

    }

    public void Play_Ambience_Cave()
    {

    }

    public void Play_Ambience_ForestSwamp()
    {

    }

    public void Play_Ambience_Factory()
    {

    }

    //distance
    public void Play_Ambient_FireFlickering()
    {

    }

    public void Play_BloomingPlantWoosh()
    {

    }

    public void Play_BioluminescentPlantLighting()
    {

    }

    public void Play_MassiveRootsMoving()
    {

    }

    public void Play_PipesSteam()
    {

    }

    public void Play_Sludge()
    {

    }
    #endregion

    #region Interaction
    public void Play_Gong()
    {
        AkSoundEngine.PostEvent("int_gong", gameObject);
    }
    public void Play_PurchaseNoise()
    {

    }

    public void Play_WoodenCartWheels_Dirt()
    {

    }

    public void Play_MushroomBounce()
    {

    }

    public void Play_WaterSplash()
    {

    }

    public void Play_EyeBoing()
    {
        AkSoundEngine.PostEvent("int_boing", gameObject);
    }

    /* redundant with cordyceps going underground  public void Play_GateFall()
      // {

      }
   */
    public void Play_LeafPile()
    {

    }

    public void Play_SmashingMulch()
    {

    }
    #endregion

    #region NPC

    public void Play_Fenway()
    {
        AkSoundEngine.PostEvent("Fenway", gameObject);
    }

    public void Play_Rootford()
    {
        AkSoundEngine.PostEvent("Rootford", gameObject);
    }

    public void Play_Catkin()
    {
        AkSoundEngine.PostEvent("Catkin_Test", gameObject);
    }

    //Labeled Play_Her in the doc
    public void Play_Ms_Stamen()
    {
        AkSoundEngine.PostEvent("Her", gameObject);
    }

    public void Play_Strawberry()
    {
        AkSoundEngine.PostEvent("Strawberry", gameObject);
    }

    public void Play_Banan()
    {

    }

    public void Play_Gourdo()
    {
        AkSoundEngine.PostEvent("Gourdo", gameObject);

    }
    public void Play_Peabody()
    {
        AkSoundEngine.PostEvent("Peabody", gameObject);

    }

    public void Play_Spud()
    {

    }

    public void Play_Mulchant()
    {
        AkSoundEngine.PostEvent("Mulchant", gameObject);
    }

    public void Play_Buddy()
    {
        AkSoundEngine.PostEvent("Buddy", gameObject);
    }

    public void Play_CarrotSlug()
    {
        AkSoundEngine.PostEvent("CarrotSlug", gameObject);
    }

    public void Play_Pedalton()
    {
        AkSoundEngine.PostEvent("Pedalton", gameObject);
    }

    public void Play_Cactus()
    {
        AkSoundEngine.PostEvent("Cactus", gameObject);
    }
   

    public void Play_WillowTree()
    {
        AkSoundEngine.PostEvent("Willowtree", gameObject);
    }



    public void Play_Lizard_Walk()
    {

    }

    public void Play_Tree_EyeOpening()
    {
        AkSoundEngine.PostEvent("stg_angel_tree_awakened", gameObject);

    }
    #endregion

    #region Stinger
    public void Play_Cordyceps_GoingUnderground()
    {
        //   AkSoundEngine.PostEvent("stg_growth_function", gameObject);
        AkSoundEngine.PostEvent("int_cordyceps", gameObject);

    }

    public void Play_ClearingBranches()
    {
        //     AkSoundEngine.PostEvent("stg_growth_function", gameObject);
    }

    public void Play_Mulch_Smash()
    {
        AkSoundEngine.PostEvent("stg_obtain_mulch", gameObject);
    }

    public void Play_Acorn_Pickup()
    {
        AkSoundEngine.PostEvent("stg_acorn_pickup", gameObject);
    }
    #endregion

    #region UI
    public void Play_UI_Click_MainMenu()
    {
        AkSoundEngine.PostEvent("Click", gameObject);
    }

    public void Play_Stinger_Start_MainMenu()
    {
        AkSoundEngine.PostEvent("Start_Button", gameObject);
    }

    public void Play_Stinger_Back_MainMenu()
    {
        AkSoundEngine.PostEvent("Back_Button", gameObject);
    }

    public void Play_UI_Click_PauseMenu()
    {
        AkSoundEngine.PostEvent("Click", gameObject);
    }

    public void Play_Stinger_Start_Cutscene()
    {
        AkSoundEngine.PostEvent("Start_Opening_Cinematic", gameObject);
    }
    public void Play_Stinger_Start_EndCutscene()
    {
        AkSoundEngine.PostEvent("Start_EndAnamatic", gameObject);
    }

    #endregion

    #region Switch Boards for Multiple Sound Types

    /// <summary>
    /// To help simplify and consolidate the walking sound calls so that
    /// the AudioManager is the only place this switch is defined
    /// </summary>
    /// <param name="surfaceType"></param>
    public void Play_Walk(WalkSurface surfaceType)
    {
        switch (surfaceType)
        {
            case WalkSurface.NONE:
                Debug.Log("Shush");
                break;
            case WalkSurface.GRASS:
                Play_Walk_Grass();
                break;
            case WalkSurface.STONE:
                Play_Walk_Stone();
                break;
            case WalkSurface.DIRT:
                Play_Walk_Dirt();
                break;
            case WalkSurface.MUD:
                Play_Walk_Mud();
                break;
            case WalkSurface.WOOD:
                Play_Walk_Wood();
                break;
            case WalkSurface.MUSHROOM:
                Play_Walk_Mushroom();
                break;
            case WalkSurface.LEAF:
                Play_Walk_Leaf();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// To help simplify and consolidate the headbutt sound calls so that
    /// the AudioManager is the only place this switch is defined
    /// </summary>
    /// <param name="surfaceType"></param>
    public void Play_Headbutt(HeadbuttSurface surfaceType)
    {
        switch (surfaceType)
        {
            case HeadbuttSurface.NONE:
                Debug.Log("Shush");
                break;
            case HeadbuttSurface.WOOD:
                Play_Headbutt_Wood();
                break;
            case HeadbuttSurface.STONE:
                Play_Headbutt_Stone();
                break;
            case HeadbuttSurface.GAZEGROWTH:
                Play_EyeBoing();
                break;
            case HeadbuttSurface.MULCH:
                Play_SmashingMulch();
                break;
        }
    }

    /// <summary>
    /// To help simplify and consolidate the on contact sound calls so that
    /// the AudioManager is the only place this switch is defined
    /// </summary>
    /// <param name="contactType"></param>
    public void Play_OnContact(ContactType contactType)
    {
        switch (contactType)
        {
            case ContactType.MUSHROOM_BOUNCE:
                Play_MushroomBounce();
                break;
            case ContactType.WATER_SPLASH:
                Play_WaterSplash();
                break;
            case ContactType.LEAF_PILE:
                Play_LeafPile();
                break;
        }
    }

    #endregion
}

public enum WalkSurface
{
    NONE, GRASS, STONE, DIRT, MUD, WOOD, MUSHROOM, LEAF
}

public enum HeadbuttSurface
{
    NONE, WOOD, STONE, GAZEGROWTH, MULCH
}

public enum ContactType
{
    MUSHROOM_BOUNCE, WATER_SPLASH, LEAF_PILE
}
