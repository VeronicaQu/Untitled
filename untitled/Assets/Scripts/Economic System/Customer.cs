using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // ==============   variables   ==============
    //mood vars
    public enum Mood {Angry, Neutral, Happy}
    private Mood myMood = Mood.Neutral;
    public Mood mood {get{return myMood;}}
    [SerializeField] private int myLeniency;
    
    //order vars
    private List<string> myOrder;
    private float myOrderPrice;

    //timer vars
    [SerializeField] private float myHappyWaitTime;
    [SerializeField] private float myNeutralWaitTime;
    [SerializeField] private float myAngryWaitTime;
    private Timer waitTimer;
    TimeManager tm;

    //econ vars
    [SerializeField] private float myTipPercent;
    [SerializeField] private float myGenerousTipPercent;
    private Economy econ;


    // ==============   methods   ==============
    private void Awake(){
        econ = FindObjectOfType<Economy>();
        tm = FindObjectOfType<TimeManager>();
        waitTimer = new Timer(myHappyWaitTime, EndTimerHandler);
    }

    public void Init(){

    }

    public void CheckOrder(List<Ingredient> given){
        int wrongIngredient = myOrder.Count;
        foreach(Ingredient i in given){
            if (myOrder.Remove(i.name)){
                wrongIngredient--;
                given.Remove(i);
            }
        }
        wrongIngredient += given.Count;
        HandleAfterOrder(wrongIngredient);
    }

    private void HandleAfterOrder(int wrongIngredient){
        if (wrongIngredient > myLeniency){
            myMood = Mood.Angry;
        }
        else PayForOrder(); 
        //UpdateGenerator();
    }
    private void PayForOrder(){ //only pay for order if the number of wrong/missing ingredients is acceptable to customer
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
        tm.AddTimer(waitTimer);
    }

    public void EndTimerHandler(){

    }
}
