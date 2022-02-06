using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private List <Timer> timers = new List<Timer>();

    public void Update(){
        
    }
    public void AddTimer(Timer t){
        timers.Add(t);
    }
    public void RemoveTimer(Timer t){
        timers.Remove(t);
    }

    private void decrementTimers(){ //decrement all timers

    }
}
