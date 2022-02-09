using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private GameObject popUpMessagePrefab;
    [SerializeField] private GameObject myGameCanvas;
    public GameObject gameCanvas{get{return myGameCanvas;}}
    public GameObject popUpMessageBase{get{return popUpMessagePrefab;}}


    // ==============   methods   ==============
    void Start()
    {
        
    }
    
    void Update()
    {

    }
}
