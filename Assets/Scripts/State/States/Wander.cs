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
        owner.atDestination.value = false;

        circle = Random.insideUnitCircle * Random.Range(10f, 20f);

        owner.navigation.targetNode = null;
        owner.agentMovement.Resume();

        target = owner.transform.position;
        target.x += circle.x;
        target.z += circle.y;

        owner.agentMovement.moveTowards(target);
        owner.atDestination.value = Vector3.Distance(owner.transform.position, target) <= 0;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
