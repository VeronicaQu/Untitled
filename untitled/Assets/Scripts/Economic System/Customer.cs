using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // ==============   variables   ==============
    //mood vars
    public enum Mood {Angry, Neutral, Happy}
    private Mood myMood = Mood.Happy;
    public Mood mood {get{return myMood;}}
    [SerializeField] private int myLeniency;
    
    //order vars
    [SerializeField] private List<string> myOrder;
    [SerializeField] private float myOrderPrice;

    //timer vars
    [SerializeField] private float myHappyWaitTime;
    [SerializeField] private float myNeutralWaitTime;
    [SerializeField] private float myAngryWaitTime;
    private Timer waitTimer;
    GameManager gm;

    //econ vars
    [SerializeField] private float myTipPercent;
    [SerializeField] private float myGenerousTipPercent;
    private Economy econ;


    // ==============   methods   ==============
    private void Awake(){
        econ = FindObjectOfType<Economy>();
        gm = FindObjectOfType<GameManager>();
        waitTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        waitTimer.Init(myHappyWaitTime, EndTimerHandler);
    }

    public void Init(){

    }

    public void CheckOrder(List<string> given){
        int wrongIngredient = myOrder.Count;
        List<string> check = new List<string>(given);
        foreach(string i in given){
            if (myOrder.Remove(i)){
                wrongIngredient--;
                check.Remove(i);
            }
        }
        wrongIngredient = Mathf.Max(wrongIngredient, check.Count);
        Debug.Log("The order has " + wrongIngredient + " wrong or missing ingredients.");
        HandleAfterOrder(wrongIngredient);
    }

    private void HandleAfterOrder(int wrongIngredient){
        if (wrongIngredient > myLeniency){
            myMood = Mood.Angry;
            Debug.Log("Customer will leave without paying anything.");
        }
        else PayForOrder(); 
        //UpdateGenerator();
    }
    private void PayForOrder(){ //only pay for order if the number of wrong/missing ingredients is acceptable to customer
        Debug.Log("Customer will pay for something.");
        switch(myMood){
            case Mood.Angry:
                econ.AddPlayerCoins(myOrderPrice);
            break;
            case Mood.Neutral:
                econ.AddPlayerCoins(myOrderPrice + myOrderPrice*myTipPercent);
            break;
            case Mood.Happy:
                econ.AddPlayerCoins(myOrderPrice + myOrderPrice*myGenerousTipPercent);
            break;
        }
    }

    private void UpdateGenerator(){ //customer mood affects how often this kind of customer appears

    }

    private void StartTimer(){
        waitTimer.StartTimer();
    }

    public void EndTimerHandler(){
        waitTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        switch(myMood){
            case Mood.Happy:
                waitTimer.Init(myNeutralWaitTime, EndTimerHandler);
            break;
            case Mood.Neutral:
                waitTimer.Init(myAngryWaitTime, EndTimerHandler);
            break;
            case Mood.Angry:
                Destroy(this.gameObject);
            break;
        }
    }
}
