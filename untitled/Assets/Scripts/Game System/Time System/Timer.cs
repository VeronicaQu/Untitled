using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    float time;
    UnityAction endAction;

    public Timer (float newTime, UnityAction newAction){
        time = newTime;
        endAction = newAction;
    }
    public void SetTime(float newTime){
        time = newTime;
    }
    public float GetTime(){
        return time;
    }

    public void EndTimer(){//what to do when the timer stops?
        endAction();
    }
}
