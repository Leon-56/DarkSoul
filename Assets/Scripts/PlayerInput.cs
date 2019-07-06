using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	[Header("===== Key settings =====")]
	public string keyUp;
	public string keyDown;
	public string keyLeft;
	public string keyRight;

	public string keyA;
	public string keyB;
	public string keyC;
	public string keyD;


	[Header("===== Output signals =====")]
	public float Dup;
	public float Dright;
	public float Dmag;
	public Vector3 Dvec;
	public bool run;
	public bool jump;
	private bool lastJump;

	[Header("===== Others =====")]
	public bool inputEnable = true;

	private float targetDup;
	private float targetDright;

	private float velocityDup;
	private float velocityDright;

	private Vector2 tempDAxis;
	
	void Update () {
		//输入信号转换为二维信号
		targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
		targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
		//输入锁死
		if(inputEnable == false) {
			targetDright = 0f;
			targetDup = 0f;
		}
		//数值平滑处理
		Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
		Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

		tempDAxis = SquareToCircle(new Vector2(Dup, Dright));

		Dmag =  Mathf.Sqrt((tempDAxis.x * tempDAxis.x) + (tempDAxis.y * tempDAxis.y));
		Dvec = tempDAxis.y * transform.right + tempDAxis.x * transform.forward;

		run = Input.GetKey(keyA);

		//一次性信号Jump
		bool newJump = Input.GetKey(keyB);
		if (newJump != lastJump && newJump == true) {
			jump = true;
			//print("jump trigger!!!!!");
		}
		else {
			jump = false;
		}
		lastJump = newJump;

	}

	//二维坐标转换为球形坐标
	private Vector2 SquareToCircle(Vector2 input) {
		Vector2 output = Vector2.zero;
		output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
		output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
		return output;
	}

}
