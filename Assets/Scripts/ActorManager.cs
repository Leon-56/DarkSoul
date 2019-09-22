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

    public void DoDamage() {
        ac.IssueTrigger("hit");
    }

}
