using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // PlayerData
    public float[] position;
    public int playerAcorns;

    // GameData
    public bool mulchant_GivenBottles;

    public bool hasBrownMulch;
    public bool hasGreenMulch;
    public bool hasYellowMulch;

    public bool angelTreeAwake;
    public bool starTreeAwake;
    public bool willowTreeAwake;

    public bool revealNewAreas;


    public bool area_Tutorial;
    public bool tutorial_bus_Called;

    public bool tutorial_HasTalked_Rootford_Intro1;
    public bool tutorial_HasTalked_Rootford_Intro2;

    public bool tutorial_HasTalked_BusDriver_1;

    public GameData(GameController gameController)
    {
        playerAcorns = gameController.playerAcorns;

        position = new float[3];
        if (gameController.playerController != null)
        {
            position[0] = gameController.playerController.transform.position.x;
            position[1] = gameController.playerController.transform.position.y;
            position[2] = gameController.playerController.transform.position.z;
        }
        

        mulchant_GivenBottles = gameController.mulchant_GivenBottles;

        hasBrownMulch = gameController.hasBrownMulch;
        hasGreenMulch = gameController.hasGreenMulch;
        hasYellowMulch = gameController.hasYellowMulch;

        angelTreeAwake = gameController.angelTreeAwake;
        starTreeAwake = gameController.starTreeAwake;
        willowTreeAwake = gameController.willowTreeAwake;

        revealNewAreas = gameController.revealNewAreas;

        area_Tutorial = gameController.area_Tutorial;
        tutorial_bus_Called = gameController.tutorial_bus_Called;
        tutorial_HasTalked_Rootford_Intro1 = gameController.tutorial_HasTalked_Rootford_Intro1;
        tutorial_HasTalked_Rootford_Intro2 = gameController.tutorial_HasTalked_Rootford_Intro2;
        tutorial_HasTalked_BusDriver_1 = gameController.tutorial_HasTalked_BusDriver_1;
    }
}
