using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropObject : MonoBehaviour
{
    // ==============   variables   ==============
    enum Type { //the types of drag + drop
        Base,
        Serve,
        Cooker
    };
    [SerializeField] private Type myType;
    

    [SerializeField] private Animator myAnimator;
    private Player player;

    private CustomerManager cm;
    private GameManager gm;
    
    private Timer myTimer;

    //cooker
    [SerializeField] private float myCookingTime;
    private Ingredient myIngredient;
    [SerializeField] private int maxState;

    private SharedArea myArea;
    public SharedArea area{set{myArea = value;}}

    // ==============   methods   ==============
    private void Awake(){
        player = FindObjectOfType<Player>();
        cm = FindObjectOfType<CustomerManager>();
        gm = FindObjectOfType<GameManager>();
    }

    public void OnMouseDown(){
        Debug.Log(this.name);
        switch (myType){
            case Type.Base:
                if (!player.handFree) player.AddToCurrentOrder();
                else{
                    //pick up base
                    if (myArea != null) myArea.HandlePickUp();
                    player.PickUpItem(this.gameObject);
                }
            break;

            case Type.Serve:
                if (player.holdingBase){
                    Debug.Log("serving");
                    cm.ServeCustomer(player.order);
                }
            break;

            //tools
            case Type.Cooker:
                if (!player.handFree) StartCooker();
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

    //tools
    //cooker
    private void StartCooker(){
        GameObject i = player.DropItem("ingredient"); //see if the player is holding an ingredient
        if (i == null) return;
        myIngredient = i.GetComponent<Ingredient>(); //it's never the case that the ingredient placed here is the wrong kind
        
        if (myIngredient.state > maxState){ //don't cook items that are already cooked
            player.PickUpItem(myIngredient.gameObject);
            return;
        }
        //start timer
        myIngredient.gameObject.SetActive(false);
        myTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        myTimer.Init(myCookingTime, HandleFinishedCooking);
        myTimer.StartTimer();

        //move ingredient into place
    }

    private void HandleFinishedCooking(){
        Destroy(myTimer.gameObject);
        if (myIngredient != null) myIngredient.ChangeState();
        myIngredient.gameObject.SetActive(true);
    }
}
