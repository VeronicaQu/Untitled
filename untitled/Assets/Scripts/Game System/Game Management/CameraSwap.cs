using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwap : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private List <CinemachineVirtualCamera> virtualCams;
    public int camIndex {get; private set;}
    //public int index{get{return camIndex;}}
    public int maxCamIndex {get{return virtualCams.Count;}}
    [SerializeField] private CameraButton leftButton;
    [SerializeField] private CameraButton rightButton;

    // ==============   methods   ==============
    void Start(){
        camIndex = 1;
        SwapToCam(camIndex);
    }

    void Update()
    {
        CheckMoveInput();
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
        if (camIndex == n) return;
        int c = camIndex;
        switch (n){
            case 0: //left -> don't show left arrow
            leftButton.ResetScale();
            leftButton.gameObject.SetActive(false);
            break;

            case 1: //mid -> show both arrows
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            break;

            case 2: //right -> don't show right arrow
            rightButton.ResetScale();
            rightButton.gameObject.SetActive(false);
            break;
        }
        virtualCams[n].Priority = 11;
        virtualCams[c].Priority = 10;
        camIndex = n;
    }
}
