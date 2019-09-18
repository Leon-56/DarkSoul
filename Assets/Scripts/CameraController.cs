using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	public IUserInput pi;
	public float horizontalSpeed = 100.0f;
	public float verticalSpeed = 80.0f;
	public float cameraDampValue = 0.05f;
	public Image lockDot;
	public bool lockState;
	public bool IsAI = false;

	private GameObject playerHandle;
	private GameObject cameraHandle;
	private GameObject model;
	private GameObject camera1;
	private float tempEulerX;
	private Vector3 cameraMoveVolecity;
	[SerializeField]
	private LockTarget lockTarget;
	
	void Start () {
		cameraHandle = transform.parent.gameObject;
		playerHandle = cameraHandle.transform.parent.gameObject;
		tempEulerX = 20.0f;
		ActorContorller ac = playerHandle.GetComponent<ActorContorller> ();
		model = ac.model;
		pi = ac.pi;

		if(!IsAI) {
			camera1 = Camera.main.gameObject;
			lockDot.enabled = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		lockState = false;

	}
	
	
	void FixedUpdate () {

		if(lockTarget == null) {
			Vector3 tempModelEuler = model.transform.eulerAngles;

			playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
			tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
			tempEulerX = Mathf.Clamp(tempEulerX, -40, 50);
			cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

			model.transform.eulerAngles = tempModelEuler;
		}
		else {
			Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
			tempForward.y = 0f;
			playerHandle.transform.forward = tempForward;
			//lockDot.transform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position);
			cameraHandle.transform.LookAt(lockTarget.obj.transform);
		}

		if(!IsAI) {
			camera1.transform.position = Vector3.SmoothDamp(camera1.transform.position, transform.position, ref cameraMoveVolecity, cameraDampValue);
			//camera1.transform.eulerAngles = transform.eulerAngles;
			camera1.transform.LookAt(cameraHandle.transform);
		}
	}

	void Update() {
		if(lockTarget != null) {
			if(!IsAI) {
				lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
			}
			if(Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f) {
				LockProcessA(null, false, false, IsAI);
			}
		}
	}

	private void LockProcessA(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI) {
		lockTarget = _lockTarget;

		if(!_isAI) {
			lockDot.enabled = _lockDotEnable;
		}

		lockState = _lockState;
	}

	private class LockTarget {
		public GameObject obj;
		public float halfHeight;
		public LockTarget(GameObject obj, float halfHeight) {
			this.obj = obj;
			this.halfHeight = halfHeight;
		}
	}

	public void LockUnlock() {

			Vector3 modelOrigin1 = model.transform.position;
			Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
			Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
			Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(IsAI ? "Player" : "Enemy"));

			if(cols.Length == 0) {
				LockProcessA(null, false, false, IsAI);
			}
			else {
				foreach(var col in cols) {
					if(lockTarget != null && lockTarget.obj == col.gameObject) {
						LockProcessA(null, false, false, IsAI);
						break;
					}
					LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, IsAI);
					break;
				}
			}
	}

}
