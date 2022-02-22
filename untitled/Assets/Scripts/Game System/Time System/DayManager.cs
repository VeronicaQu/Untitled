using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    // ==============   variables   ==============
    //time stages
    Timer dayTimer;
    private float[] timePerStage;
    [SerializeField] Sprite[] timeStageImages;
    private int stageIndex;

    //FIX: delete
    [SerializeField] Image tempIcon;
    [SerializeField] Sprite dayIcon;
    [SerializeField] Sprite nightIcon;

    [SerializeField] Text timeText;

    GameManager gm;
    EventManager em;

    // ==============   methods   ==============
    private void Awake(){
        em = FindObjectOfType<EventManager>();
        em.OnLocationChange += UpdateOnLocationChange;
        gm = FindObjectOfType<GameManager>();

        dayTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
    }

    private void UpdateOnLocationChange(Location next){
        Debug.Log("called Update on Location in Day Manager");
        dayTimer.StopTimer();
        //update the generator
        timePerStage = next.timeStages;
        ResetVars();
    }

    private void ResetVars(){
        stageIndex = 0;
        if (stageIndex < timePerStage.Length) Init(timePerStage[stageIndex]);
    }

    private void HandleDayChange(){
        //FIX: change the visuals
        tempIcon.sprite = nightIcon;

        if (stageIndex < timePerStage.Length) Init(timePerStage[stageIndex]);
    }

    private void Init(float time){
        em.ChangeTime(time, stageIndex++); //let subscribers know time has changed
        //dayTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        dayTimer.Init(time, HandleDayChange, timeText);
        dayTimer.StartTimer();
    }
}
