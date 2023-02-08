using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Chase(StateAgent owner) : base(owner) { }

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
        if (owner.seen.Length == 0) owner.machine.StartState(nameof(Idle));
        else
        {
            owner.agentMovement.moveTowards(owner.seen[0].transform.position);

            Vector3 direction = owner.seen[0].transform.position - owner.agentMovement.transform.position;
            float angle = Vector3.Angle(owner.transform.forward, direction);
            float distance = Vector3.Distance(owner.seen[0].transform.position, owner.agentMovement.transform.position);

            if (distance < 4 && angle < owner.agentView.max_angle)
            {
                owner.machine.StartState(nameof(Attack));
            }
        }
    }
}
