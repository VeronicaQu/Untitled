using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    // ==============   variables   ==============
    private bool isMoving;
    private Tool myTool;
    public Tool tool {set{myTool = value;}}


    // ==============   functions   ==============
    public void ValidateMotion(){

    }
    public void ResetVars(){
        isMoving = false;
    }
}
