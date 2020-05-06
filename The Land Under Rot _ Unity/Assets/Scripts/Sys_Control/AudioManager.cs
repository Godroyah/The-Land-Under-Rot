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
        AkSoundEngine.PostEvent("char_footsteps_grass", gameObject);
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
        AkSoundEngine.PostEvent("char_falling", gameObject);
    }

    public void Play_Landing_OnFeet()
    {

    }

    public void Play_Jump()
    {

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

    }

    //Labeled Play_Her in the doc
    public void Play_Ms_Stamen()
    {
        AkSoundEngine.PostEvent("Her", gameObject);
    }

    public void Play_Strawberry()
    {

    }

    public void Play_Banan()
    {

    }

    public void Play_Gourdo()
    {

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

    }

    public void Play_Pedalton()
    {

    }

    public void Play_Cactus()
    {

    }

    public void Play_WillowTree()
    {

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
    #endregion
}

