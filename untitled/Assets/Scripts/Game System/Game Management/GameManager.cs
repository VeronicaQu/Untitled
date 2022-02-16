using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private GameObject customerOrderParent;
    public Transform orderParent {get{return customerOrderParent.transform;}}
    
    [SerializeField] private GameObject myTimerPrefab;
    public GameObject timerPrefab{get{return myTimerPrefab;}}

    [SerializeField] private GameObject myPopUpMessagePrefab;
    [SerializeField] public GameObject popUpMessagePrefab{get{return myPopUpMessagePrefab;}}

    [SerializeField] private GameObject myGameCanvas;
    [SerializeField] public GameObject gameCanvas{get{return myGameCanvas;}}


    // ==============   methods   ==============
    
    void Start()
    {
        
    }
    
    void Update()
    {
        //restart();
        CheckRestart();

    }

    private void CheckRestart(){
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
}
