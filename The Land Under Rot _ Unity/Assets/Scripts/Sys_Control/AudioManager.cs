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

    }

    public void Play_Walk_Stone()
    {

    }

    public void Play_Walk_Dirt()
    {

    }

    public void Play_Walk_Mud()
    {

    }

    public void Play_Walk_Wood()
    {

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

    public void Play_Headbutt_Wood()
    {

    }

    public void Play_Headbutt_Stone()
    {

    }

    public void Play_Headbutt_Enemy()
    {

    }

    public void Play_Headbutt_EnemyRecoil()
    {

    }

    public void Play_Falling()
    {

    }

    public void Play_Landing_OnFeet()
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
    //public void Play_Ambient_BugsAroundLight()
    //{

    //}

    ////unless these are specific bugs, no distance needed
    //public void Play_Ambient_Bugs()
    //{

    //}

    ////unless this is specific water in the location, no distance needed
    //public void Play_Ambient_RunningWater()
    //{

    //}

    ////distance needed if there are specific birds in the scene 
    //public void Play_Ambient_BirdsChirping()
    //{

    //}

    ////same as last ones, depends if theres specific water 
    //public void Play_Ambience_Water()
    //{

    //}

    //public void Play_Ambience_Cave()
    //{

    //}

    //public void Play_Ambience_ForestSwamp()
    //{

    //}

    //public void Play_Ambience_Factory()
    //{

    //}

    ////distance
    //public void Play_Ambient_FireFlickering()
    //{

    //}

    //public void Play_BloomingPlantWoosh()
    //{

    //}

    //public void Play_BioluminescentPlantLighting()
    //{

    //}

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

    }

    public void Play_GateFall()
    {

    }

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

    }

    public void Play_Catkin()
    {

    }

    //Ms_Stamen
    public void Play_Her()
    {

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

    }

    public void Play_Buddy()
    {

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

    }
#endregion

    #region Stinger
    public void Play_Cordyceps_GoingUnderground()
    {

    }

    public void Play_ClearingBranches()
    {

    }

    public void Play_Mulch_Smash()
    {

    }

    public void Play_Acorn_Pickup()
    {

    }
    #endregion

    #region UI
    public void Play_UI_Click_MainMenu()
    {

    }

    public void Play_UI_Click_PauseMenu()
    {

    }
    #endregion
}
