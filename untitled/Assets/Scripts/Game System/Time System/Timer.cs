using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer: MonoBehaviour
{
    private float time;
    UnityAction endAction;
    IEnumerator myCoroutine;

    public void init (float newTime, UnityAction newAction){
        endAction = newAction;
        time = newTime;
    }

    public void AddToTimer(float t){
        time += t;
    }

    public void StartTimer(){
        myCoroutine = DecrementTimer();
        StartCoroutine(myCoroutine);
    }

    private IEnumerator DecrementTimer(){ //coroutine for timer
        while (time > 0){
            time -= 1;
            yield return new WaitForSeconds(1);
        }
        endAction();
        Destroy(this);
    }
}
