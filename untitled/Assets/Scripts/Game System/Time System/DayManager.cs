using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    Timer dayTimer;
    [SerializeField] float timeTilChange = 10;

    Image tempIcon;
    [SerializeField] Sprite dayIcon;
    [SerializeField] Sprite nightIcon;

    [SerializeField] Text timeText;

    GameManager gm;

    private void Start(){
        gm = FindObjectOfType<GameManager>();
        tempIcon = GetComponent<Image>();

        tempIcon.sprite = dayIcon;
        Init(timeTilChange);
    }

    private void HandleDayChange(){
        if (tempIcon.sprite == dayIcon) tempIcon.sprite = nightIcon;
        else tempIcon.sprite = dayIcon;
        Init(timeTilChange);
    }

    private void Init(float time){
        dayTimer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
        dayTimer.Init(time, HandleDayChange, timeText);
        dayTimer.StartTimer();
    }
}
