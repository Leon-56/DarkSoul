using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testJoystick : MonoBehaviour {
    void Update()
    {
        print("btn6:" + Input.GetAxis("btn6"));
        // print("padV:" + Input.GetAxis("padV"));
        //print("RT:" + Input.GetAxis("RT"));
    }
}
