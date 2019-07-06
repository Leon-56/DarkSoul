using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorContorller : MonoBehaviour {

	public GameObject model;
	public PlayerInput pi;
	public float walkSpeed = 1.5f;
	public float runSpeed = 2.5f;

	[SerializeField]
	private Animator anim;
	private Rigidbody rigid;
	private Vector3 planarVec;
	private Vector3 thrustVec;

	[SerializeField]
	private bool lockPanar = false;

	void Awake () {
		pi = GetComponent<PlayerInput>();
		anim = model.GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
	}
	
	void Update () {
		anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2.0f : 1.0f), 0.5f));
		if (pi.jump) {
			anim.SetTrigger("jump");
		}
		//设置角色朝向
		if (pi.Dmag > 0.1f) {
			Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.25f);
			model.transform.forward = targetForward;
		}
		if(!lockPanar)
			planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runSpeed : 1.0f);
	}

	void FixedUpdate() {
		//rigid.position += planarVec * Time.fixedDeltaTime * walkSpeed ;
		rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
		thrustVec = Vector3.zero;
	}

	public void OnJumpEnter() {
		pi.inputEnable = false;
		lockPanar = true;
		thrustVec = new Vector3(0, 3.0f, 0);
	}

	public void OnJumpExit() {
		pi.inputEnable = true;
		lockPanar = false;
	}

}
