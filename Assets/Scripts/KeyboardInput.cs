using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput {

	[Header("===== Key settings =====")]
	public string keyUp;
	public string keyDown;
	public string keyLeft;
	public string keyRight;

	public string keyA = "left shift";
	public string keyB = "space";
	public string keyC = "mouse 0";
	public string keyD = "mouse 1";

	public string keyJUp;
	public string keyJDown;
	public string keyJLeft;
	public string keyJRight;

	public MyButton buttonA = new MyButton();
	public MyButton buttonB = new MyButton();
	public MyButton buttonC = new MyButton();
	public MyButton buttonD = new MyButton();

	[Header("===== Mouse settings=====")]
	public bool mouseEnable = true;
	public float mouseSensitivityX = 1.5f;
	public float mouseSensitivityY = 1.2f;

	private Vector2 tempDAxis;
	
	void Update () {

		buttonA.Tick(Input.GetKey(keyA));
		buttonB.Tick(Input.GetKey(keyB));
		buttonC.Tick(Input.GetKey(keyC));
		buttonD.Tick(Input.GetKey(keyD));

		//输入信号转换为二维信号
		targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
		targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

		if (mouseEnable) {
			Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY;
			Jright = Input.GetAxis("Mouse X") * mouseSensitivityX;
		}
		else {
			Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
			Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
		}

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

		run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
		jump = buttonA.OnPressed && buttonA.IsExtending;
		roll = buttonA.OnReleased && buttonA.IsDelaying;

		defense = buttonD.IsPressing;
		rb = buttonC.OnPressed;

	}

}
