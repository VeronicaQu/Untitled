using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraButton : Button, IPointerClickHandler
{
    CameraSwap cs;
    private void Awake(){
        cs = FindObjectOfType<CameraSwap>();
    }
    public void OnPointerClick(PointerEventData eventData){
        if (gameObject.name == "left")
            cs.SwapToCam(cs.camIndex-1 >=0 ? cs.camIndex-1: cs.camIndex);
        if (gameObject.name == "right")
            cs.SwapToCam(cs.camIndex+1 < cs.maxCamIndex ? cs.camIndex+1: cs.camIndex);
    }
}
