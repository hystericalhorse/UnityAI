using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    private float timer = 0;

    public Patrol(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.agentMovement.Resume();
        timer = Random.Range(5, 10);
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) owner.machine.StartState(nameof(Wander));
        if (owner.seen.Length > 0)
        {
            owner.machine.StartState(nameof(Chase));
        }
    }
}
