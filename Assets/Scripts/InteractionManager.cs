using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;
    
    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager> ();

    void Start()
    {
        interCol = GetComponent<CapsuleCollider> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager> ();
        foreach (var ecastm in ecastms)
        {
            if (!overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Add(ecastm);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager> ();
        foreach (var ecastm in ecastms)
        {
            if (!overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Remove(ecastm);
            }
        }
    }

}
