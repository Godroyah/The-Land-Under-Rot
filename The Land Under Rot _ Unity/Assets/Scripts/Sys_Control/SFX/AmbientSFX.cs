using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmbienceType {NONE, BUGS, BUGS_AROUND_LIGHT, WATER, RUNNING_WATER,
    BIRDS_CHIRPING, FIRE_FLICKERING, GLOWING_PLANT}

//CAVE, FOREST_SWAMP, FACTORY, PIPE_STEAM, SLUDGE

public class AmbientSFX : MonoBehaviour
{
    Transform player;

    public AmbienceType ambientSound;

    public float playerProximity;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        switch(ambientSound)
        {
            case AmbienceType.NONE:
                Debug.Log("Shush");
                break;
            case AmbienceType.BUGS:
                AudioManager.Instance.Play_Ambient_Bugs();
                break;
            case AmbienceType.BUGS_AROUND_LIGHT:
                AudioManager.Instance.Play_Ambient_BugsAroundLight();
                break;
            case AmbienceType.WATER:
                AudioManager.Instance.Play_Ambience_Water();
                break;
            case AmbienceType.RUNNING_WATER:
                AudioManager.Instance.Play_Ambient_RunningWater();
                break;
            case AmbienceType.BIRDS_CHIRPING:
                AudioManager.Instance.Play_Ambient_BirdsChirping();
                break;
            case AmbienceType.FIRE_FLICKERING:
                AudioManager.Instance.Play_Ambient_FireFlickering();
                break;
            case AmbienceType.GLOWING_PLANT:
                AudioManager.Instance.Play_BioluminescentPlantLighting();
                break;
            //case AmbienceType.PIPE_STEAM:
            //    AudioManager.Instance.Play_PipesSteam();
            //    break;
            //case AmbienceType.SLUDGE:
            //    AudioManager.Instance.Play_Sludge();
            //    break;
            //case AmbienceType.CAVE:
            //    AudioManager.Instance.Play_Ambience_Cave();
            //    break;
            //case AmbienceType.FOREST_SWAMP:
            //    AudioManager.Instance.Play_Ambience_ForestSwamp();
            //    break;
            //case AmbienceType.FACTORY:
            //    AudioManager.Instance.Play_Ambience_Factory();
            //    break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerProximity = Vector3.Distance(player.position, transform.position);
    }
}
