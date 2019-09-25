using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MySuperPlayableMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        ActorManager trackBinding = playerData as ActorManager;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount ();
        float tempSum = 0;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<MySuperPlayableBehaviour> inputPlayable = (ScriptPlayable<MySuperPlayableBehaviour>)playable.GetInput(i);
            MySuperPlayableBehaviour input = inputPlayable.GetBehaviour ();
            
            // Use the above variables to process each frame of this playable.
            tempSum += input.myFloat * inputWeight;
        }
    }
}
