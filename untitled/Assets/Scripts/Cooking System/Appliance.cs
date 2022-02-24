using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Appliance : MonoBehaviour
{
    enum Type { //the types of drag + drop
        TimedCarb
    };
    [SerializeField] private Type myType;

    [SerializeField] private Animator myAnimator;
    private Timer myTimer;

    //cooker
    [SerializeField] private float myCookingTime;
    private Ingredient myIngredient;
    [SerializeField] private int maxState;
    [SerializeField] private Text timedCarbText;

    private Player player;
    private GameManager gm;
    
    private void Awake(){
        player = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
        myTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
    }

    //if the player presses this object, what should happen?
    public void OnMouseDown(){
        switch (myType){
            case Type.TimedCarb: //if the type is timed carb, start its associated function
                if (!player.handFree) StartCarbTimed();
            break;
        }
    }

    //indicate hovered tool to player
    public void OnMouseOver(){
        if (myAnimator == null || myTimer !=null) return;
        //set start state and play animation
    }
    public void OnMouseExit(){
        if (myAnimator == null || myTimer !=null) return;
        //set end state and play animation
    }

    //cooker
    private void StartCarbTimed(){
        GameObject i = player.DropItem("ingredient"); //see if the player is holding an ingredient
        if (i == null) return;
        myIngredient = i.GetComponent<Ingredient>(); 
        
        //if wrong ingredient
        if (myIngredient.type != Ingredient.Type.Carb || myIngredient.state > maxState){ //don't cook items that are already cooked
            player.PickUpItem(myIngredient.gameObject);
            return;
        }
        //start timer
        myIngredient.gameObject.SetActive(false);
        myTimer.Init(myCookingTime, HandleFinishedTimedCooking, timedCarbText);
        myTimer.StartTimer();
        //TODO: move ingredient into place
    }

    //what to do when the timed carb tool is finished
    private void HandleFinishedTimedCooking(){
        if (myIngredient != null) myIngredient.ChangeState();
        myIngredient.gameObject.SetActive(true);
    }
}
