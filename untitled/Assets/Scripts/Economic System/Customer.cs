using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum Mood {Angry, Neutral, Happy}
    private Mood myMood = Mood.Neutral;
    public Mood mood {get{return myMood;}}
    [SerializeField]private float myHappyWaitTime;
    [SerializeField]private float myNeutralWaitTime;
    [SerializeField]private float myAngryWaitTime;
    private Timer waitTimer;
    TimeManager tm;
    

    [SerializeField] private float myTip;
    private Economy econ;

    private void Awake(){
        econ = FindObjectOfType<Economy>();
        tm = FindObjectOfType<TimeManager>();
        waitTimer = new Timer(myHappyWaitTime, EndTimerHandler);
    }

    public void Init(){

    }

    public void CheckOrder(){

    }
    private void StartTimer(){
        tm.AddTimer(waitTimer);
    }

    public void EndTimerHandler(){

    }
}
