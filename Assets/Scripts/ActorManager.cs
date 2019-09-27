using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

    public ActorContorller ac;
    
    [Header("=== Auto Generate if Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;

    void Awake() {
        ac = GetComponent<ActorContorller> ();

        GameObject model = ac.model;
        GameObject sensor = null;
        try{
            sensor = transform.Find("sensor").gameObject;
        }catch (System.Exception ex) {
            //
            // If there is no "sensor" object.
            //
        }
        bm = Bind<BattleManager> (sensor);
        wm = Bind<WeaponManager> (model);
        sm = Bind<StateManager> (gameObject);
        dm = Bind<DirectorManager> (gameObject);
        im = Bind<InteractionManager> (sensor);
        

        ac.OnAction += DoAction;
    }

    public void DoAction() {
        if(im.overlapEcastms.Count != 0) {
            if(im.overlapEcastms[0].active == true && !dm.IsPlaying())
            {
                if(im.overlapEcastms[0].eventName == "frontStab") {
                    dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
                }
                else if(im.overlapEcastms[0].eventName == "openBox") {
                    if(BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 30))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am.transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("openBox", this, im.overlapEcastms[0].am);
                    }
                }
                else if(im.overlapEcastms[0].eventName == "leverUp") {
                    if(BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 30))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am.transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("leverUp", this, im.overlapEcastms[0].am);
                    }
                }
            }
        }
    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface {
        T tempInstance;
        if(go == null)
            return null;
        
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

    public void LockUnlockActorController(bool value) {
        ac.SetBool("lock", value);
    }


}
