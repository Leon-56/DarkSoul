using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("===== Output signals =====")]
	public float Dup;
	public float Dright;
	public float Jup;
	public float Jright;
	public float Dmag;
	public Vector3 Dvec;
    // precessing singal
	public bool run;
    public bool defense;
    // once singal
	public bool jump;
	protected bool lastJump;
	//public bool attack;
	protected bool lastAttack;
	public bool roll;
	public bool lockon;
	public bool lb;
	public bool lt;
	public bool rb;
	public bool rt;	

	[Header("===== Others =====")]
	public bool inputEnable = true;

	protected float targetDup;
	protected float targetDright;
    protected float velocityDup;
	protected float velocityDright;

    //二维坐标转换为球形坐标
	protected Vector2 SquareToCircle(Vector2 input) {
		Vector2 output = Vector2.zero;
		output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
		output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
		return output;
	}
    
}
