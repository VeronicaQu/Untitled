using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Location : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // ==============   variables   ==============
    [SerializeField] private GameObject myLocation; //holds all child objects related to this location
    private Vector3 myScale;
    private Map map;
    private Vector3 hoverScale = new Vector3(0.3f,0.3f,0.3f);
    private bool activeLocation;
    [SerializeField] private float myPrice;
    public float relocationPrice{get{return myPrice;}}
    
    // ==============   methods   ==============
    void Awake(){
        myScale = this.transform.localScale;
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
    public void ChangeScale(Vector3 deltaScale){
        //scale up/down by scale change
        this.transform.localScale += deltaScale;
    }
    public void ResetScale(){
        this.transform.localScale = myScale;
    }

    public void OnPointerClick(PointerEventData eventData){
        if (activeLocation) return;
        if (map != null){
            map.selectedLocation = this;
            map.ValidateRelocation();
        }
    }

    //indicate hovered location to player
    public void OnPointerEnter(PointerEventData eventData){
        if (activeLocation) return;
        ChangeScale(hoverScale);
    }
    public void OnPointerExit(PointerEventData eventData){
        if (activeLocation) return;
        ResetScale();
    }
}
