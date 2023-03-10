using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private float timer = 0;

    public Idle(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {

        timer = Random.Range(2, 4);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (owner.seen.Length > 0) owner.machine.StartState(nameof(Chase));
        timer -= Time.deltaTime;
        if (timer <= 0) owner.machine.StartState(nameof(Patrol));

    }
}
