using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorContorller : MonoBehaviour {

	public GameObject model;
	public CameraController camcon;
	public IUserInput pi;
	public float walkSpeed = 1.5f;
	public float runSpeed = 2.5f;

	[SerializeField]
	public Animator anim;
	private Rigidbody rigid;
	private Vector3 planarVec;
	private Vector3 thrustVec;
	private float lerpTarget;

	[SerializeField]
	private bool lockPanar = false;
	private bool trackDirection = false;
	private bool canAttack = true;
	private Vector3 deltaPos;

	public bool leftIsShield = true;

	public delegate void OnActionDelegate();
	public event OnActionDelegate OnAction;

	void Awake () {
		IUserInput[] inputs = GetComponents<IUserInput>();
		foreach(var input in inputs) {
			if(input.enabled == true) {
				pi = input;
				break;
			}
		}
		anim = model.GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
	}
	
	void Update () {

		if(pi.lockon) {
			camcon.LockUnlock();
		}

		if(camcon.lockState == false) {
			anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2.0f : 1.0f), 0.5f));
			anim.SetFloat("right", 0);
		}
		else {
			Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
			anim.SetFloat("forward", localDvec.z * (pi.run ? 2.0f : 1.0f));
			anim.SetFloat("right", localDvec.x * (pi.run ? 2.0f : 1.0f));
		}

		//anim.SetBool("defense", pi.defense);

		if(pi.roll || rigid.velocity.magnitude > 7f) {
			anim.SetTrigger("roll");
			canAttack = false;
		}

		if (pi.jump) {
			anim.SetTrigger("jump");
			canAttack = false;
		}

		//attack
		if ((pi.rb || pi.lb) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack) {
			if(pi.rb) {
				anim.SetBool("R0L1", false);
				anim.SetTrigger("attack");
			}
			else if(pi.lb && !leftIsShield) {
				anim.SetBool("R0L1", true);
				anim.SetTrigger("attack");
			}
		}

		//
		if((pi.rt || pi.lt) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack) {
			if(pi.rt) {
				// do right heavy attack

			}
			else {
				if(!leftIsShield) {
					//do left heavy attack

				}
				else {
					anim.SetTrigger("counterBack");
				}

			}
		}

		if(pi.action) {
			OnAction.Invoke ();
		}

		if(leftIsShield) {
			if(CheckState("ground") || CheckState("block")) {
				anim.SetBool("defense", pi.defense);
				anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
			}
			else {
				anim.SetBool("defense", false);
				anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
			}
		}
		else {
			anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
		}

		if(camcon.lockState == false) {
			//设置角色朝向
			if (pi.Dmag > 0.1f) {
				Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.25f);
				model.transform.forward = targetForward;
			}
			if(!lockPanar)
				planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runSpeed : 1.0f);
			}
		else {
			if(trackDirection == false)
				model.transform.forward = transform.forward;
			else 
				model.transform.forward = planarVec.normalized;
				
			if(!lockPanar)
				planarVec = pi.Dvec * walkSpeed * (pi.run ? runSpeed : 1.0f);
		}

	}

	void FixedUpdate() {
		//rigid.position += planarVec * Time.fixedDeltaTime * walkSpeed ;
		rigid.position += deltaPos;
		rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
		thrustVec = Vector3.zero;
		deltaPos = Vector3.zero;
	}

	public bool CheckState(string stateName, string layerName = "Base Layer") {
		int layerIndex = anim.GetLayerIndex(layerName);
		bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
		return result;
	}

	public bool CheckStateTag(string tagName, string layerName = "Base Layer") {
		int layerIndex = anim.GetLayerIndex(layerName);
		bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
		return result;
	}

	///
	/// Messaga processing block
	///
	public void OnJumpEnter() {
		pi.inputEnable = false;
		lockPanar = true;
		thrustVec = new Vector3(0, 3.5f, 0);
		trackDirection = true;
	}

	public void IsGround() {
		//print("Is Ground");
		anim.SetBool("isGround", true);
	}

	public void IsNotGround() {
		//print("Is Not Ground");
		anim.SetBool("isGround", false);
	}

	public void OnGroundEnter() {
		pi.inputEnable = true;
		lockPanar = false;
		canAttack = true;
		trackDirection = false;
		model.SendMessage("CounterBackDisable");
	}

	public void OnFallEnter() {
		pi.inputEnable = false;
		lockPanar = true;
	}

	public void OnRollEnter() {
		pi.inputEnable = false;
		lockPanar = true;
		trackDirection = true;
	}

	// public void OnRollUpdate() {
	// 	thrustVec = thrustVec = model.transform.forward * anim.GetFloat("rollVelocity");
	// }

	public void OnJabEnter() {
		pi.inputEnable = false;
		lockPanar = true;
	}

	public void OnJabUpdate() {
		thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
	}

	public void OnAttack1hAEnter() {
		pi.inputEnable = false;
		//lerpTarget = 1.0f;
	}

	public void OnAttack1hAUpdate() {
		thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
		//float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
		//currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
		//anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
	}

	public void OnAttackExit() {
		model.SendMessage("WeaponDisable");
	}

	public void OnUpdateRM(object _deltaPos) {
		//print("111111");
		if (CheckState("attack1hC") || CheckState("attack1hC 0")) {
			deltaPos += (deltaPos + (Vector3)_deltaPos) / 2.0f;
		}
	}

	public void OnHitEnter() {
		pi.inputEnable = false;
		//planarVec = Vector3.zero;
		model.SendMessage("WeaponDisable");
		model.SendMessage("CounterBackDisable");
	}

	public void OnBlockEnter() {
		pi.inputEnable = false;
	}

	public void OnDieEnter() {
		pi.inputEnable = false;
		planarVec = Vector3.zero;
		model.SendMessage("WeaponDisable");
		model.SendMessage("CounterBackDisable");
	}

	public void OnStunnedEnter() {
		pi.inputEnable = false;
		planarVec = Vector3.zero;
	}

	public void OnCounterBackEnter() {
		pi.inputEnable = false;
		planarVec = Vector3.zero;
	}

	public void IssueTrigger(string triggerName) {
		anim.SetTrigger(triggerName);
	}

	public void SetBool(string boolName, bool value) {
		anim.SetBool(boolName, value);
	}

}
