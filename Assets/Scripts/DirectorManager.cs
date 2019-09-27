using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector pd;

    [Header("=== Timeline assets ===")]
    public TimelineAsset frontStab;
    public TimelineAsset openBox;
    public TimelineAsset leverUp;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;
    public ActorManager victim;


    void Start()
    {
        pd = GetComponent<PlayableDirector> ();
        pd.playOnAwake = false;
    }

    public bool IsPlaying() {
        if(pd.state == PlayState.Playing)
            return true;
        return false;
    }

    public void PlayFrontStab(string timelineName, ActorManager attacker, ActorManager victim)
    {
        

        if(timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;


            foreach (var track in timeline.GetOutputTracks())
            {
                if(track.name == "Attacker Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, attacker);
                    }
                }
                else if(track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, victim);
                    }
                }
                else if(track.name == "Attacker Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if(track.name == "Victim Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Evaluate();

            pd.Play();
        }

        else if(timelineName == "openBox")
        {
            pd.playableAsset = Instantiate(openBox);

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;


            foreach (var track in timeline.GetOutputTracks())
            {
                if(track.name == "Player Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, attacker);
                    }
                }
                else if(track.name == "Box Script")
                {
                    pd.SetGenericBinding(track, victim);
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, victim);
                    }
                }
                else if(track.name == "Player Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if(track.name == "Box Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Evaluate();

            pd.Play();
        }

        else if(timelineName == "leverUp")
        {
            pd.playableAsset = Instantiate(leverUp);

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;


            foreach (var track in timeline.GetOutputTracks())
            {
                if(track.name == "Player Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, attacker);
                    }
                }
                else if(track.name == "Lever Script")
                {
                    pd.SetGenericBinding(track, victim);
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehave = myclip.template;
                        pd.SetReferenceValue(myclip.myActor.exposedName, victim);
                    }
                }
                else if(track.name == "Player Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if(track.name == "Lever Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Evaluate();

            pd.Play();
        }
    }

}
