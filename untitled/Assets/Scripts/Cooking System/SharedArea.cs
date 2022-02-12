using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedArea: MonoBehaviour
{
    [SerializeField] private bool ingredientsOnly;
    private GameObject myItem;

    private bool freeArea = true;
    private Player player;

    private void Awake(){
        player = FindObjectOfType<Player>();
    }
    

    public void OnMouseDown(){
        Debug.Log(this.name);
        if(!player.handFree){
            if (freeArea) PlaceObjectOnShared();
        }
        else if(!freeArea){
            HandlePickUp();
        }
    }

    public void OnMouseOver(){
        //turn green if free area; else red
    }

    public void OnMouseExit(){
        //turn off alpha
    }

    //shared board
    private void PlaceObjectOnShared(){
        if (ingredientsOnly){
            myItem = player.DropItem("ingredient");
            if (myItem == null) return;
            myItem.GetComponent<Ingredient>().area = this;
        }
        else{
            myItem = player.DropItem("tool");
            if (myItem == null) return;
            myItem.GetComponent<Tool>().area = this;
        }
        freeArea = false;
        myItem.transform.position = this.transform.position;
    }

    public void HandlePickUp(){
        freeArea = true;
        myItem = null;
    }
}
