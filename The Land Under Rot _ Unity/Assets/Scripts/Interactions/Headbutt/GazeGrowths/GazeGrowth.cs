using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeGrowth : Interactable
{
    public GazeGrowthType growthType;

    protected SphereCollider thisDetector;

    protected GameController gameController;
    public Animator animator;


    private void Start()
    {
        gameController = GameController.Instance;
    }

    public override void Interact()
    {
        base.Interact();
    }
}

public enum GazeGrowthType
{
    Blue, Red, Green, Yellow
}

public enum GG_Anim
{
    Gaze_Hit_Trigger, Gaze_Cry_Bool
}
