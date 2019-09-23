using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface {

    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

    void Start() {

        whL = transform.DeepFind("WeaponHandleL").gameObject;
        whR = transform.DeepFind("WeaponHandleR").gameObject;
        weaponColR = whR.GetComponentInChildren<Collider> ();
        weaponColL = whL.GetComponentInChildren<Collider> ();
        weaponColR.enabled = false;
        weaponColL.enabled = false;

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);

    }

    public WeaponController BindWeaponController(GameObject targetObj) {
        
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController> ();

        if(tempWc == null) {
            tempWc = targetObj.AddComponent<WeaponController> ();
        }
        tempWc.wm = this;

        return tempWc;

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

    public void CounterBackEnable() {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable() {
        am.SetIsCounterBack(false);
    }

}
