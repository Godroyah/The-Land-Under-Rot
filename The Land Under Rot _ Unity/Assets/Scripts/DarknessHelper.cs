using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessHelper : MonoBehaviour
{
    public Darkness parentDarkness;
    private int darknessLevel;

    public void SetDarknessLevel(int level)
    {
        darknessLevel = level;
    }
    
    public void ApplyDarkness()
    {
        Debug.Log("Entered darkness: " + darknessLevel);
        parentDarkness.AdjustBlinder(darknessLevel);
    }

    public void RemoveDarkness()
    {
        Debug.Log("leaving darkness: " + darknessLevel);
        parentDarkness.AdjustBlinder(darknessLevel - 1);
    }
}
