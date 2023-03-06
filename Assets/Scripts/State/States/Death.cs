using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Death : State
{
    public Death(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.animator.SetBool("isAlive", false);
        owner.agentMovement.Stop();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        //if (owner.animationDone) GameObject.Destroy(owner.gameObject, 2);
    }
}
