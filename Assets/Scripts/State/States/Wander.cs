using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State
{

    private Vector3 target;
    private Vector2 circle;
    public Wander(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        circle = Random.insideUnitCircle * Random.Range(10f, 20f);

        owner.navigation.targetNode = null;
        owner.agentMovement.Resume();

        target = owner.transform.position;
        target.x += circle.x;
        target.z += circle.y;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        Debug.DrawLine(owner.transform.position, target);
        owner.agentMovement.moveTowards(target);
        if (owner.agentMovement.vel.magnitude == 0)
        {
            owner.machine.StartState(nameof(Idle));
        }
    }
}
