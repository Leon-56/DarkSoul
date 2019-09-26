using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager myActor;
    public float myFloat;
    //PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart (Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();
    }

    public override void OnGraphStop (Playable playable)
    {
        // if(pd != null)
        //     pd.playableAsset = null;
    }

    public override void OnBehaviourPlay (Playable playable, FrameData info)
    {

    }

    public override void OnBehaviourPause (Playable playable, FrameData info)
    {
        myActor.LockUnlockActorController(false);
    }

    public override void PrepareFrame (Playable playable, FrameData info)
    {
        myActor.LockUnlockActorController(true);
    }

}
