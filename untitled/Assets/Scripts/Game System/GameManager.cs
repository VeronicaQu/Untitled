using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpMessagePrefab;
    [SerializeField] private GameObject myGameCanvas;
    public GameObject gameCanvas{get{return myGameCanvas;}}
    public GameObject popUpMessageBase{get{return popUpMessagePrefab;}}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
