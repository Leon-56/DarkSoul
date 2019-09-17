using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput {

    private Vector2 tempDAxis;

    [Header("===== Joystick Settings =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";
    public string btn0 = "btn0";
    public string btn1 = "btn1";
    public string btn2 = "btn2";
    public string btn3 = "btn3";
	public string btnRT = "axis9";
	public string btnRB = "";
	public string btnLT = "axis10";
	public string btnLB = "";
	public string btnJstick = "btn9";

    
	public MyButton buttonA = new MyButton();
	public MyButton buttonB = new MyButton();
	public MyButton buttonC = new MyButton();
	public MyButton buttonD = new MyButton();
	public MyButton buttonRT = new MyButton();
	public MyButton buttonRB = new MyButton();
	public MyButton buttonLT = new MyButton();
	public MyButton buttonLB = new MyButton();
	public MyButton buttonJstick = new MyButton();


    void Update() {

		buttonA.Tick(Input.GetButton(btn0));
		buttonB.Tick(Input.GetButton(btn1));
		buttonC.Tick(Input.GetButton(btn2));
		buttonD.Tick(Input.GetButton(btn3));
		buttonRB.Tick(Input.GetButton(btnRB));
		buttonRT.Tick(Input.GetButton(btnRT));
		buttonLB.Tick(Input.GetButton(btnLB));
		buttonLT.Tick(Input.GetButton(btnLT));
		buttonJstick.Tick(Input.GetButton(btnJstick));
		//print(buttonJstick.OnPressed);

        Jup = -1 * Input.GetAxis(axisJup);
		Jright = Input.GetAxis(axisJright);

        //输入信号转换为二维信号
		targetDup = Input.GetAxis(axisY);
		targetDright = Input.GetAxis(axisX);

		//输入锁死
		if(inputEnable == false) {
			targetDright = 0f;
			targetDup = 0f;
		}

        //数值平滑处理
		Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
		Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

		tempDAxis = SquareToCircle(new Vector2(Dright, Dup));

		Dmag =  Mathf.Sqrt((tempDAxis.x * tempDAxis.x) + (tempDAxis.y * tempDAxis.y));
		Dvec = tempDAxis.x * transform.right + tempDAxis.y * transform.forward;

        run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
		jump = buttonA.OnPressed && buttonA.IsExtending;
		roll = buttonA.OnReleased && buttonA.IsDelaying;

		defense = buttonD.IsPressing;
		//attack = buttonC.OnPressed;
		rb = buttonRB.OnPressed;
		rt = buttonRT.OnPressed;
		lb = buttonLB.OnPressed;
		lt = buttonLT.OnPressed;
		lockon = buttonJstick.OnPressed;

    }
    
}
