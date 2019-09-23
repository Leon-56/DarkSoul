using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

    public ActorContorller ac;
    
    [Header("=== Auto Generate if Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;

    void Awake() {
        ac = GetComponent<ActorContorller> ();

        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        
        bm = Bind<BattleManager> (sensor);
        wm = Bind<WeaponManager> (model);
        sm = Bind<StateManager> (gameObject);

    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface {
        T tempInstance;
        tempInstance = go.GetComponent<T> ();
        if(tempInstance == null) {
            tempInstance = go.AddComponent<T> ();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    public void SetIsCounterBack(bool value) {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid) {
        // if(sm.HP > 0)
        //     sm.AddHP(-5);

        if(sm.isCounterBackSucess) {
            if(counterValid)
                targetWc.wm.am.Stunned();
        }
        else if(sm.isCounterBackFailure) {
            if(attackValid)
                HitOrDie(false);
        }
        else if(sm.isImmortal) {
        }
        else if(sm.isDefense) {
            // Attack should be blocked.
            Block();
        }
        else {
            if(attackValid)
                HitOrDie(true);
        }
    }

    public void HitOrDie(bool doHitAnimation) {
        if(sm.HP == 0) {
                // Alread dead.
            }
            else {
                sm.AddHP(-5);
                if(sm.HP <= 0) {
                    Die();
                }
                else {
                    if(doHitAnimation) {
                        Hit();
                    }
                    // do some VFX, like splatter blood
                }
            }
    }

    public void Stunned() {
        ac.IssueTrigger("stunned");
    }

    public void Block() {
        ac.IssueTrigger("block");
    }

    public void Hit() {
        if(sm.HP > 0)
            ac.IssueTrigger("hit");
    }

    public void Die() {
        ac.IssueTrigger("die");
        ac.pi.inputEnable = false;
        if(ac.camcon.lockState == true) {
            ac.camcon.LockUnlock();
        }
        ac.camcon.enabled = false;
    }

}
