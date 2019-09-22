using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface {

    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject WhL;
    public GameObject WhR;

    void Start() {
        WhL = transform.DeepFind("WeaponHandleL").gameObject;
        WhR = transform.DeepFind("WeaponHandleR").gameObject;
        weaponColR = WhR.GetComponentInChildren<Collider> ();
        weaponColL = WhL.GetComponentInChildren<Collider> ();
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }

    public void WeaponEnable() {
        if(am.ac.CheckStateTag("attackL")) {
            weaponColL.enabled = true;
        }
        else {
            weaponColR.enabled = true;
        }
    }

    public void WeaponDisable() {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }

}
