using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton {

    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    public float extendingDuration = 0.15f;
    public float delayingDuration  = 0.15f;


    private bool curState = false;
    private bool lastState = false;

    private MyTimer exitTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool input) {

        exitTimer.Tick();
        delayTimer.Tick();

        curState = input;

        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (curState != lastState) {
            if(curState == true) {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else {
                OnReleased = true;
                StartTimer(exitTimer, extendingDuration);
            }
        }
        lastState = curState;

        if(exitTimer.state == MyTimer.STATE.RUN)
            IsExtending = true;

        if(delayTimer.state == MyTimer.STATE.RUN)
            IsDelaying = true;

    }

    private void StartTimer(MyTimer timer, float duration) {
        timer.duration = duration;
        timer.Go();
    }

}
