using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ==============   variables   ==============
    private Tool heldTool;
    private Ingredient heldIngredient;
    private bool isMyHandFree;
    public bool isHandFree {get{return isMyHandFree;}}
    

    // ==============   functions   ==============
    public void PickUpItem(GameObject item){

    }

    private void UseHeldItem(){

    }

    private void DropIngredient(){

    }

}
