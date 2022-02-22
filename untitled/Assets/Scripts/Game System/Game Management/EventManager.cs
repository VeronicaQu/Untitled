using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // ==============   variables   ==============
    //location change
    public delegate void LocationChangeEvent (Location next);
    public event LocationChangeEvent OnLocationChange;

    //day time change
    public delegate void TimeChangeEvent (float nextTime, int phase);
    public event TimeChangeEvent OnTimeChange;

    // ==============   methods   ==============
    public void ChangeLocation(Location next){
        Debug.Log("called location change in Event Manager");
        if (OnLocationChange != null){
            Debug.Log("On location change has subs");
            OnLocationChange(next); //if there is a subscriber
        }
    }

    public void ChangeTime(float nextTime, int phase){
        if (OnTimeChange != null) OnTimeChange(nextTime, phase);
    }
}
