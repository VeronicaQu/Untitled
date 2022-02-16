using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private Animator myCustomerAnim; 
    //mood vars
    public enum Mood {Angry, Neutral, Happy}
    private Mood myMood = Mood.Happy;
    public Mood mood {get{return myMood;}}
    [SerializeField] private int myLeniency;
    
    //order vars
    [SerializeField] private List<Ingredient> tempIng; //FIX: DELETE
    [SerializeField] private List<string> myOrder = new List<string>();
    [SerializeField] private float myOrderPrice;
    [SerializeField] private Image[] orderUi;
    private int orderUiIndex;

    //timer vars
    [SerializeField] private float myHappyWaitTime;
    [SerializeField] private float myNeutralWaitTime;
    [SerializeField] private float myAngryWaitTime;
    private Timer waitTimer;
    GameManager gm;

    //econ vars
    [SerializeField] private float myTipPercent;
    [SerializeField] private float myGenerousTipPercent;
    [SerializeField] private Economy econ;


    // ==============   methods   ==============
    public void Awake(){
        econ = FindObjectOfType<Economy>();
        gm = FindObjectOfType<GameManager>();
        
        waitTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        waitTimer.Init(myHappyWaitTime, EndTimerHandler);
        
        //FIX: DELETE
        Init();
        foreach(Ingredient i in tempIng){
            AddToOrder(i.initialSprite, i.name, i.price);
        }

        //ensure inactive
        this.gameObject.SetActive(false);
    }

    public void Init(){
        //FIX: "spawn" character -> need a way to sort them smaller when they spawn
        //and move them up as customers leave (like theyre in a line)
        myCustomerAnim.SetTrigger("MoveToFront");
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

    public void AddToOrder(Sprite s, string i, float p){ //add an ingredient to this order and update the UI
        UpdateOrderUI(s);
        myOrder.Add(i);
        //Debug.Log(myOrder.Count);
        myOrderPrice += p;
    }

    private void UpdateOrderUI(Sprite s){
        if (orderUiIndex < orderUi.Length) orderUi[orderUiIndex++].sprite = s;
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
        waitTimer.StartTimer();
    }

    private void Leave(){
        Destroy(myCustomerAnim.gameObject);
        Destroy(this.gameObject);
    }
}
