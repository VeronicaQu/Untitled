using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // ==============   variables   ==============
    public Text tempOrderText;
    private bool isHoldingBase;
    public bool holdingBase{get{return isHoldingBase;}}
    
    [SerializeField] private Tool heldTool;
    [SerializeField] private Ingredient heldIngredient;
    [SerializeField] private GameObject heldItem;

    private ToolLine myToolLine;
    public ToolLine toolLine {set{myToolLine = value;}}
    
    private bool isHandFree = true;
    public bool handFree {get {return isHandFree;}}

    [SerializeField] private List <string> currentOrder = new List <string>();
    public List<string> order {get {return currentOrder;}}

    //vars to create protein
    private float endTime;
    private bool canCreateProtein;
    private int wantedProtein;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1
         //FIX: add keycodes as needed
     };

    CameraManager cam;
    ProteinManager pm;

    Vector3 mousePos;

    // ==============   functions   ==============
    private void Awake(){
        cam = FindObjectOfType<CameraManager>();
        pm = GetComponent<ProteinManager>();
    }
    public void Update(){
        UpdateMouseItem();
        CheckInput();
    }

    private void UpdateMouseItem(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (heldItem !=null) heldItem.transform.position = mousePos;
    }

    private void CheckInput(){
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadSceneAsync("MainScene");
        }

        ValidateCreateProtein();
    }

    private void ValidateCreateProtein(){
        for(int n = 0; n < keyCodes.Length; n++){
            if (Input.GetKeyDown(keyCodes[n]) && !canCreateProtein) {
                canCreateProtein = true;
                float startTime = Time.time;
                endTime = startTime + pm.countdown;
                Debug.Log(string.Format("start time: %d, end time: %d", startTime, endTime));

                pm.AnimateCreateProtein();
            }
            if (Input.GetKeyUp(keyCodes[n]) && canCreateProtein) {
                canCreateProtein = false;
                pm.StopAnim();
            }
            if (Time.time >= endTime && canCreateProtein) {
                canCreateProtein = false;
                pm.CreateProtein(n);
                pm.StopAnim();
            }
        } 
    }

    public void PickUpItem(GameObject item){ //pick up an item, and sort it into the correct script type
        HandleHasItem();
        
        heldItem = item;
        heldItem.GetComponent<Collider2D>().enabled = false;
        
        if (item.GetComponent<Ingredient>()){
            heldIngredient = item.GetComponent<Ingredient>();
        }
        else if(item.GetComponent<Tool>()){
            heldTool = item.GetComponent<Tool>();
        }
        else {
            HandleBase();
        }
    }

    public GameObject DropItem(string type){ //drop the item thats held, if its type matches
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
        else if (type == "base"){
            held = heldItem;
            heldItem = null;
        }

        if (heldItem == null) HandleNoItems(); //if no item is being held
        if (held != null) held.GetComponent<Collider2D>().enabled = true; //enable collider
        
        return held;
    }

    private void HandleNoItems(){
        heldTool = null;
        heldIngredient = null;
        heldItem = null;
        
        isHandFree = true;
        cam.ShowButtons();
    }

    private void HandleHasItem(){
        isHandFree = false;
        cam.HideButtons();
    }

    public void AddToCurrentOrder(){ //add held ingredient to the order
        if (heldIngredient!= null && heldIngredient.AtEndState()){
            currentOrder.Add(heldIngredient.name);
            UpdateOrderUI(heldIngredient.name); //FIX: delete
            Destroy(heldIngredient.gameObject);
            HandleNoItems();
        }
    }

    public void ClearOrder(){
        Debug.Log(currentOrder.Count);
        currentOrder.Clear();
        tempOrderText.text = "Order:";//FIX: delete
    }

    private void UpdateOrderUI(string name){ //FIX: delete
        tempOrderText.text = tempOrderText.text + "  " + name;
    }

    public void HandleBase(){ //pick up or drop the base object if the player is holding 
        if (isHoldingBase){
            isHoldingBase = false;
            //drop base
        }
        else{
            isHoldingBase = true;
            //pick up base
        }
    }

    public void ValidateToolLines(Ingredient i){ //validate by checking if player is holding the required tool
        if (heldTool != null && heldTool.ValidateMotion(i)) {
            heldTool.AddToHovered(i);
            i.ValidateToolLines();
        }
    }
}
