using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public MySuperPlayableBehaviour template = new MySuperPlayableBehaviour ();
    public ExposedReference<GameObject> myCamera;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MySuperPlayableBehaviour>.Create (graph, template);
        MySuperPlayableBehaviour clone = playable.GetBehaviour ();
        clone.myCamera = myCamera.Resolve (graph.GetResolver ());
        return playable;
    }
}
