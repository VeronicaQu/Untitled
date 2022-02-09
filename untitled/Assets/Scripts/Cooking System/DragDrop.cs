using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // ==============   variables   ==============
    enum Type { //the types of drag + drop
        Base,
        Serve,
        Cooker
    };
    [SerializeField] private Type myType;
    private Animator myAnimator;
    private Player player;
    private CustomerManager cm;

    // ==============   methods   ==============
    private void Awake(){
        player = FindObjectOfType<Player>();
        cm = FindObjectOfType<CustomerManager>();
    }

    public void OnPointerClick(PointerEventData eventData){
        switch (myType){
            case Type.Base:
                if (!player.handFree) player.AddToCurrentOrder();
                else{
                    //pick up base
                    player.HandleBase();
                }
            break;
            case Type.Serve:
                if (player.holdingBase) cm.ServeCustomer(player.order);
            break;
            case Type.Cooker:
            break;
            //any other click/drag + drop items go here
        }
    }

    //indicate hovered tool to player
    public void OnPointerEnter(PointerEventData eventData){
        if (myAnimator == null) return;
        //set start state and play animation
    }
    public void OnPointerExit(PointerEventData eventData){
        if (myAnimator == null) return;
        //set end state and play animation
    }
}
