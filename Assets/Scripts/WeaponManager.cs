using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface {

    [SerializeField]
    private Collider weaponColL;
    [SerializeField]
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

    void Awake() {

        try {
            whL = transform.DeepFind("WeaponHandleL").gameObject;
            weaponColL = whL.GetComponentInChildren<Collider> ();
            wcL = BindWeaponController(whL);
            weaponColL.enabled = false;
        }catch(System.Exception ex) {
            //
            // If there is no "weaponHandleL" or related objects.
            //
        }

        try {
            whR = transform.DeepFind("WeaponHandleR").gameObject;
            weaponColR = whR.GetComponentInChildren<Collider> ();
            wcR = BindWeaponController(whR);
            weaponColR.enabled = false;
        }catch(System.Exception ex) {
            //
            // If there is no "weaponHandleR" or related objects.
            //
        }


    }

    public void UpdateCollider(string side, Collider col) {
        if(side == "L") {
            weaponColL = col;
        }
        else if(side == "R") {
            weaponColR = col;
        }
    }

    public void UnloadWeapon(string side) {
        if(side == "R") {
            foreach(Transform tran in whR.transform) {
                weaponColR = null;
                wcR.wdata = null;
                Destroy(tran.gameObject);
            }
        }
        else if(side == "L") {
            foreach(Transform tran in whL.transform) {
                weaponColL = null;
                wcL.wdata = null;
                Destroy(tran.gameObject);
            }
        }
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

    public void ChangeDualHands(bool dualOn) {
        am.ChangeDualHands(dualOn);
    }

}
