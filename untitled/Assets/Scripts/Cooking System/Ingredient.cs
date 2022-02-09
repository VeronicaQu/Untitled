using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private string name;
    [SerializeField] private Sprite[] imageStates;
    private int myImageState = 0; //initial state is 0
    [SerializeField] private int motionsToStateChange; //needed number of motions to change state
    [SerializeField] private List<Motion> motionUis; 
    private int myMotionsLeft; //number of motions left until state change -> resets to neededMotions

    public int motionsLeft {get{return myMotionsLeft;}}


    // ==============   methods   ==============
    public void ChangeState(){
        myImageState ++;
    }

    public bool AtEndState(){
        if (myImageState == imageStates.Length-1) return true;
        return false;
    }
}
