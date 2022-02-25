using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // ==============   variables   ==============
    //references
    private Player player;
    private SharedArea myArea;
    public SharedArea area{set{myArea = value;}}


    //type of ingredient
    public enum Type {Base, Carb, Vegetable, Protein}
    [SerializeField] private Type myType = Ingredient.Type.Vegetable;
    public Type type {get{return myType;}}

    //personalized variables for each kind of non-protein (order checking)
    [SerializeField] private string myName;
    public new string name {get{return myName;}}
    [SerializeField] private float myPrice;
    public float price {get{return myPrice;}}

    //image states
    [SerializeField] private Sprite[] imageStates;
    private int myImageState = 0; //initial state is 0
    public int state{get{return myImageState;}}
    public Sprite initialSprite {get{return imageStates[0];}}
    private SpriteRenderer mySpriteRenderer;

    //player interaction
    [SerializeField] private int motionsToStateChange = 1; //needed number of motions to change state
    private int myMotionsLeft; //number of motions left until state change -> resets to neededMotions
    public int motionsLeft {get{return myMotionsLeft;}}

    //tool and visual lines
    [SerializeField] private Tool.Required myRequired = Tool.Required.None;
    public Tool.Required required {get{return myRequired;}}
    private List<ToolLine> myToolLines = new List<ToolLine>();
    private bool hovered;

    // ==============   methods   ==============
    private void Start(){
        player = FindObjectOfType<Player>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        foreach (Transform child in transform){
            myToolLines.Add(child.GetComponent<ToolLine>());
        }

        myMotionsLeft = motionsToStateChange;
        InactivateToolLines();
    }

    //pick up item
    private void OnMouseDown(){ //if the player isnt holding anything, pick up this ingredient
        if (!player.handFree) return;
        //Debug.Log(this.gameObject.name);
        
        if (myArea != null) myArea.HandlePickUp();
        player.PickUpItem(this.gameObject);
        InactivateToolLines();
    }
    
    //check if this ingredient is on a cutting board and accepts the tool held by player
    private void OnMouseEnter(){
        if (AtEndState()) return;
        if (!hovered && myArea !=null && myArea.type == SharedArea.AreaType.CuttingBoard && !player.handFree){
            hovered = true;
            player.ValidateToolLines(this);
            if (myArea.type == SharedArea.AreaType.CuttingBoard) ActivateToolLines();
        }
    }

    public void ValidateToolLines(){ //allows tool lines to be used (green)
        foreach (ToolLine t in myToolLines){
            t.canClick = true;
        }
    }

    private void InvalidateToolLines(){ //invalidates tool lines (red)
        foreach (ToolLine t in myToolLines){
            t.canClick = false;
        }
        hovered = false;
    }

    private void ActivateToolLines(){ //show tool lines
        foreach (ToolLine t in myToolLines){
            t.gameObject.SetActive(true);
        }
    }
    private void InactivateToolLines(){
        foreach (ToolLine t in myToolLines){ //hide tool lines
            t.gameObject.SetActive(false);
        }
    }

    public void RemoveToolLine(ToolLine t){
        myToolLines.Remove(t);
    }
    
    public void ChangeState(){ //check if the image state of the object needs to be changed based on motions used
        if (myImageState >= imageStates.Length) return;
        myMotionsLeft--;
        if (myMotionsLeft == 0) {
            myMotionsLeft = motionsToStateChange;
            myImageState++;
            mySpriteRenderer.sprite = imageStates[myImageState];
        }
    }

    public bool AtEndState(){ //check if this ingredient has reached its end state
        if (myImageState == imageStates.Length-1) return true;
        return false;
    }

    public void ResetVars(){ //reset some variables: tool lines, 
        InactivateToolLines();
        InvalidateToolLines();
    }
}
