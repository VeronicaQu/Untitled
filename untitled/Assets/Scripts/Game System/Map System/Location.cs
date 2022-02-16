using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Location : Button, IPointerClickHandler
{
    // ==============   variables   ==============
    //location variations
    private List <Ingredient> baseIngredientPrefabs;
    public List <Ingredient> baseIngredients {get{return baseIngredientPrefabs;}}
    [SerializeField] private List <Ingredient> myIngredientPrefabs;
    public List<Ingredient> ingredients {get{return myIngredientPrefabs;}}
    private List <Customer> customerPrefabs;
    public List <Customer> customers {get{return customerPrefabs;}}

    //location settings
    [SerializeField] private GameObject myLocation; //holds all child objects related to this location
    private Map map;
    private bool activeLocation;

    //indication settings
    private Vector3 myHoverScale = new Vector3(0.3f, 0.3f, 0.3f);
    
    //econ vars
    [SerializeField] private float myPrice;
    public float relocationPrice{get{return myPrice;}}
    
    // ==============   methods   ==============
    void Awake(){
        map = FindObjectOfType<Map>();
    }
    public void SetLocation(){
        myLocation.SetActive(true);
        activeLocation = true;
    }
    public void UnsetLocation(){
        myLocation.SetActive(false);
        activeLocation = false;
    }

    public void OnPointerClick(PointerEventData eventData){
        if (activeLocation) return;
        if (map != null){
            map.selectedLocation = this;
            map.ValidateRelocation();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData){
        if (activeLocation) return;
        base.ChangeScale(myHoverScale);
    }

    public override void OnPointerExit(PointerEventData eventData){
        if (activeLocation) return;
        base.ResetScale();
    }
}
