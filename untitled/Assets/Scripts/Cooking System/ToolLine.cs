using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolLine: MonoBehaviour
{
    // ==============   variables   ==============
    private Player player;
    private SpriteRenderer mySpriteRend;

    private bool iCanClick;
    public bool canClick {set{iCanClick = value;}}

    [SerializeField] private float minPercentOfDist;
    private Collider2D myCollider;
    private float minDistance;
    private Vector3 initialPos;

    // ==============   functions   ==============
    private void Awake(){
        player = FindObjectOfType<Player>();
        mySpriteRend = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        minDistance = myCollider.bounds.size.y * minPercentOfDist;
    }

    private void OnMouseEnter(){
        if (player.handFree) return;
        if (iCanClick){
            mySpriteRend.color = Color.green;
        }
        else mySpriteRend.color = Color.red;
    }

    private void OnMouseExit(){
        if (player.handFree) return;
        ResetVars();
    }

    private void OnMouseDown(){
        if (player.handFree || !iCanClick) return;

        initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUpAsButton(){
        if (player.handFree || !iCanClick) return;

        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - initialPos;
        float distance = offset.sqrMagnitude;
        
        if (distance >= minDistance * minDistance){
            Ingredient i = transform.parent.GetComponent<Ingredient>();
            i.ChangeState();
            i.RemoveToolLine(this);
            Destroy(this.gameObject);
        }
        else{
            mySpriteRend.color = Color.yellow;
        }
    }

    public void ResetVars(){
        initialPos = Vector3.zero;
        mySpriteRend.color = Color.white;
    }
}
