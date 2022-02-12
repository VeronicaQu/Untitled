using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // ==============   variables   ==============
    private Motion motion;
    private bool isActive;
    public bool active {set{isActive = value;}} //?

    private SharedArea myArea;
    public SharedArea area{set{myArea = value;}}

    // ==============   functions   ==============
    public void UseTool(){

    }
}
