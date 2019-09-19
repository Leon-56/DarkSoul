using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

    public ActorContorller ac;
    public BattleManager bm;
    public WeaponManager wm;    

    void Awake() {
        ac = GetComponent<ActorContorller> ();

        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        
        bm = sensor.GetComponent<BattleManager> ();
        if(bm == null) {
            bm = sensor.AddComponent<BattleManager> ();
        }
        bm.am = this;

        wm = model.GetComponent<WeaponManager> ();
        if(wm == null) {
            wm = model.AddComponent<WeaponManager> ();
        }
        wm.am = this;

    }


    void Update() {
        
    }

    public void DoDamage() {
        ac.IssueTrigger("hit");
    }

}
