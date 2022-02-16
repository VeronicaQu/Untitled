using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void LocationChangeEvent (Location current, Location next);
    public event LocationChangeEvent OnLocationChange;

    public void changeLocation(Location current, Location next){
        if (OnLocationChange != null) OnLocationChange(current, next);
    }

}
