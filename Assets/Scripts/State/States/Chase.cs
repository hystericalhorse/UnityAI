using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Chase : State
{
    public Chase(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        if (owner.seen.Length > 0) owner.agentMovement.destination = owner.seen[0].transform.position;
        owner.timer.value = 2;
        owner.agentMovement.Resume();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
