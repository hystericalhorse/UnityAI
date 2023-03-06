using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : State
{
    public Flee(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.agentMovement.Resume();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (owner.seen.Length == 0) owner.enemySeen.value = false;
        else
        {
            owner.agentMovement.moveTowards(-owner.seen[0].transform.position);
        }
    }
}
