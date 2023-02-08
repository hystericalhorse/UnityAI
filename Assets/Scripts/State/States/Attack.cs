using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : State
{
    private float timer;

    public Attack(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.agentMovement.Stop();
        owner.animator.SetTrigger("attack");

        AnimationClip[] clips = owner.animator.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.FirstOrDefault<AnimationClip>(clip => clip.name == "SillyAttack");

        timer = (clip != null) ? clip.length : 1;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            owner.machine.StartState(nameof(Chase));
        }
    }
}
