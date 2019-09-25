using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TesterDirector : MonoBehaviour {

    public PlayableDirector pd;

    public Animator attacker;
    public Animator victim;

    void Start() {
        
    }

    void Update() {
        
        foreach(var track in pd.playableAsset.outputs) {
            if(track.streamName == "Attacker Animation") {
                pd.SetGenericBinding(track.sourceObject, attacker);
            }
            else if(track.streamName == "Victim Animation") {
                pd.SetGenericBinding(track.sourceObject, victim);
            }
        }

    }
}
