using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.1307699f, 0.6792453f, 0.03524386f)]
[TrackClipType(typeof(MySuperPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class MySuperPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MySuperPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
