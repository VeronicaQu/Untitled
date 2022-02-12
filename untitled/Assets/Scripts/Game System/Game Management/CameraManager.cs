using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private List <CinemachineVirtualCamera> virtualCams;
    public int camIndex {get; private set;}
    //public int index{get{return camIndex;}}
    public int maxCamIndex {get{return virtualCams.Count;}}
    [SerializeField] private CameraButton leftButton;
    [SerializeField] private CameraButton rightButton;
    private Player p;

    // ==============   methods   ==============
    void Start(){
        p = FindObjectOfType<Player>();
        camIndex = 1;
        SwapToCam(camIndex);
        
    }
    
    void Update()
    {
        if (p.handFree) CheckMoveInput();
    }

    private void CheckMoveInput(){
        if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)){
            SwapToCam(camIndex-1 >=0 ? camIndex-1: camIndex);
        }
        else if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){
            SwapToCam(camIndex+1 < maxCamIndex ? camIndex+1: camIndex);
        }
    }

    public void SwapToCam(int n){ //current, new
        if (!p.handFree || camIndex == n) return;
        int c = camIndex;
        virtualCams[n].Priority = 11;
        virtualCams[c].Priority = 10;
        camIndex = n;
        ShowButtons();
    }

    public void HideButtons(){
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }

    public void ShowButtons(){
        switch (camIndex){
            case 0: //left -> don't show left arrow
            leftButton.ResetScale();
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
            break;

            case 1: //mid -> show both arrows
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            break;

            case 2: //right -> don't show right arrow
            rightButton.ResetScale();
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
            break;
        }
    }
}
