using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ==============   variables   ==============
    private bool isHoldingBase;
    public bool holdingBase{get{return isHoldingBase;}}
    private Vector3 mousePos;
    
    [SerializeField] private Tool heldTool;
    [SerializeField] private Ingredient heldIngredient;
    [SerializeField] private GameObject heldItem;
    
    private bool isHandFree = true;
    public bool handFree {get {return isHandFree;}}

    private List <Ingredient> currentOrder = new List <Ingredient>();
    public List<Ingredient> order {get {return currentOrder;}}

    CameraManager cam;
    

    // ==============   functions   ==============
    private void Awake(){
        cam = FindObjectOfType<CameraManager>();
    }
    public void Update(){
    }

    public void PickUpItem(GameObject item){
        isHandFree = false;
        heldItem = item;
        cam.HideButtons();
        if (item.GetComponent<Ingredient>()){
            heldIngredient = item.GetComponent<Ingredient>();
        }
        else{
            heldTool = item.GetComponent<Tool>();
        }
    }

    private void UseHeldItem(){

    }

    public GameObject DropItem(string type){
        GameObject held = null;
        if (type == "ingredient" && heldIngredient!= null){
            held = heldItem;
            heldIngredient = null;
            heldItem = null;
            
        }
        else if (type == "tool" && heldTool!= null){
            held = heldItem;
            heldTool = null;
            heldItem = null;
        }
        if (heldItem == null){
            isHandFree = true;
            cam.ShowButtons();
        }
        return held;
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
