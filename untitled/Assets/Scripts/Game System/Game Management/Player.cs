using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ==============   variables   ==============
    private bool isHoldingBase;
    public bool holdingBase{get{return isHoldingBase;}}
    
    private Tool heldTool;
    private Ingredient heldIngredient;
    private bool isHandFree;
    public bool handFree {get{return isHandFree;}}
    private List <Ingredient> currentOrder = new List <Ingredient>();
    public List<Ingredient> order {get {return currentOrder;}}
    

    // ==============   functions   ==============
    public void Update(){

    }

    public void PickUpItem(GameObject item){

    }

    private void UseHeldItem(){

    }

    private void DropIngredient(){

    }

    public void AddToCurrentOrder(){
        if (heldIngredient!= null && heldIngredient.AtEndState()) currentOrder.Add(heldIngredient);
    }

    public void HandleBase(){
        if (isHoldingBase){
            isHoldingBase = false;
            //drop base
        }
        else{
            isHoldingBase = true;
            //pick up base
        }
    }
}
