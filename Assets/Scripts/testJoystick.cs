using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testJoystick : MonoBehaviour {
    void Update()
    {
        print("btn0:" + Input.GetAxis("btn0"));
        print("btn1:" + Input.GetAxis("btn1"));
        print("btn2:" + Input.GetAxis("btn2"));
        print("btn3:" + Input.GetAxis("btn3"));
        print("btn4:" + Input.GetAxis("btn4"));
        print("btn5:" + Input.GetAxis("btn5"));
        print("3rd axis:" + Input.GetAxis("axis3"));
        print("axis6:" + Input.GetAxis("axis6"));
        print("axisX:" + Input.GetAxis("axisX"));
        print("axisY:" + Input.GetAxis("axisY"));
        print("axis4:" + Input.GetAxis("axis4"));
        print("axis5:" + Input.GetAxis("axis5"));
        print("axis7:" + Input.GetAxis("axis7"));
        print("axis8:" + Input.GetAxis("axis8"));
        print("axis9:" + Input.GetAxis("axis9"));
        print("btn10:" + Input.GetAxis("btn10"));
        print("btn11:" + Input.GetAxis("btn11"));
    }
}
