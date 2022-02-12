using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // ==============   variables   ==============
    private Player player;

    [SerializeField] private string name;
    [SerializeField] private Sprite[] imageStates;
    private int myImageState = 0; //initial state is 0
    public int state{get{return myImageState;}}
    private SpriteRenderer mySpriteRenderer;

    [SerializeField] private int motionsToStateChange; //needed number of motions to change state
    [SerializeField] private List<Motion> motionUis; 
    private int myMotionsLeft; //number of motions left until state change -> resets to neededMotions
    public int motionsLeft {get{return myMotionsLeft;}}

    private SharedArea myArea;
    public SharedArea area{set{myArea = value;}}
    // ==============   methods   ==============
    private void Awake(){
        player = FindObjectOfType<Player>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void OnMouseDown(){
        Debug.Log(this.gameObject.name);
        if (myArea != null) myArea.HandlePickUp();
        player.PickUpItem(this.gameObject);
    }

    public void ChangeState(){
        if (myImageState >= imageStates.Length) return;
        myImageState++;
        mySpriteRenderer.sprite = imageStates[myImageState];
    }

    public bool AtEndState(){
        if (myImageState == imageStates.Length-1) return true;
        return false;
    }
}
