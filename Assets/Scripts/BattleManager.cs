using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface {

    private CapsuleCollider defCol;

    void Start() {
        defCol = GetComponent<CapsuleCollider> ();
        defCol.center = new Vector3(0, 1.0f, 0);
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
    }

    void OnTriggerEnter(Collider col) {

        WeaponController targetWc = col.GetComponentInParent<WeaponController> ();
        
        if(targetWc == null)
            return;

        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.ac.model;

        if(col.tag == "Weapon") {
                am.TryDoDamage(targetWc, CheckAngleTarget(receiver, attacker, 45), CheckAnglePlayer(receiver, attacker, 30));
        }
    }

    public static bool CheckAnglePlayer(GameObject player, GameObject target, float playerAngleLimit) {
        Vector3 counterDir = target.transform.position - player.transform.position;

        float counterAngle1 = Vector3.Angle(player.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(target.transform.forward, player.transform.forward);

        bool counterValid = (counterAngle1 < playerAngleLimit && Mathf.Abs(counterAngle2 - 180) < playerAngleLimit);
        return counterValid;
    }

    public static bool CheckAngleTarget(GameObject player, GameObject target, float targetAngleLimit) {
        Vector3 attackDir = player.transform.position - target.transform.position;

        float attackingAngle1 = Vector3.Angle(target.transform.forward, attackDir);

        bool attackValid = (attackingAngle1 < targetAngleLimit);
        return attackValid;
    }

}
