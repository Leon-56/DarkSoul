using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour {

    private Animator anim;
    public Vector3 a;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK() {

        if(anim.GetBool("defense") == false) {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowerArm.localEulerAngles += a;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler (leftLowerArm.localEulerAngles));
            
        }

    }

}
