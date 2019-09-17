using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer {

    public enum STATE {
        IDLE,
        RUN,
        FINISHED
    }
    public STATE state;
    public float duration = 0f;

    private float elapsedTime = 0f;

    public void Tick() {
        if (state == STATE.IDLE) {

        }
        else if (state == STATE.RUN) {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= duration)
                state = STATE.FINISHED;
        }
        else if (state == STATE.FINISHED) {

        }
    }

    public void Go() {
        elapsedTime = 0f;
        state = STATE.RUN;
    }

}
