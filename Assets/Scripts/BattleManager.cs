using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour {

    public ActorManager am;

    private CapsuleCollider defCol;

    void Start() {
        defCol = GetComponent<CapsuleCollider> ();
        defCol.center = new Vector3(0, 1.0f, 0);
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
    }

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Weapon") {
            am.DoDamage();
        }
    }

}
