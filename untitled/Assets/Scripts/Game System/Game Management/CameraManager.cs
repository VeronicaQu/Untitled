using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private GameObject customerView;
    [SerializeField] private GameObject orderView;

    private GameObject cam;
    [SerializeField] private List <CinemachineVirtualCamera> virtualCams;
    [SerializeField] private List <CinemachineVirtualCamera> virtualUpCams;

    public int camIndex {get; private set;}
    public int maxCamIndex {get{return virtualCams.Count;}}

    [SerializeField] private CameraButton leftButton;
    [SerializeField] private CameraButton rightButton;
    [SerializeField] private CameraButton upButton;
    [SerializeField] private CameraButton downButton;

    private CameraButton[] buttons;

    private Player p;

    // ==============   methods   ==============
    void Start(){
        p = FindObjectOfType<Player>();
        cam = FindObjectOfType<Camera>().gameObject;

        buttons = new CameraButton[] {leftButton, rightButton, upButton, downButton};
        camIndex = 1;
        //SwapToCam(camIndex);
        SwapUpDownCam();
        
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
        else if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)){
            SwapUpDownCam();
        }
        else if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)){
            SwapUpDownCam();
        }
    }

    public void SwapToCam(int n){ //current, new
        if (!p.handFree || camIndex == n || virtualCams[camIndex].Priority == 10) return;

        //swap the camera
        int c = camIndex;
        virtualCams[n].Priority = 11;
        virtualCams[c].Priority = 10;
        camIndex = n;

        ShowButtons();

        //move the customer view to be above the new cam;
        Vector3 newCustomerViewPos = new Vector3 (virtualCams[camIndex].transform.position.x, customerView.transform.position.y, 0);
        customerView.transform.position = newCustomerViewPos;

        //move order view
        Vector3 newOrderViewPos = new Vector3 (virtualCams[camIndex].transform.position.x, orderView.transform.position.y, 0);
        orderView.transform.position = newOrderViewPos;
    }
    public void SwapUpDownCam(){
        if (!p.handFree) return; //this would only be the case when the player is on bottom cam
        if (virtualCams[camIndex].Priority == 11){ //swap up to customer cam
            HideButtons();
            virtualUpCams[camIndex].Priority = 11;
            virtualCams[camIndex].Priority = 10;
            SwapUpDownButtons();
        }
        else{//swap down to game cam
            virtualCams[camIndex].Priority = 11;
            virtualUpCams[camIndex].Priority = 10;
            SwapUpDownButtons();
            ShowButtons();
        }

    }
    private void SwapUpDownButtons(){
        if (!downButton.gameObject.activeSelf){ //show down button
            downButton.gameObject.SetActive(true);
            upButton.gameObject.SetActive(false);
        }
        else{ //show up button
            downButton.gameObject.SetActive(false);
            upButton.gameObject.SetActive(true);
        }
            
    }

    public void HideButtons(){
        foreach(CameraButton c in buttons){
            c.gameObject.SetActive(false);
            c.ResetScale();
        }
    }

    public void ShowButtons(){
        switch (camIndex){
            case 0: //left -> don't show left arrow
            leftButton.ResetScale();
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
            break;

            case 1: //mid -> show both arrows
            //Debug.Log("cam index 1");
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
