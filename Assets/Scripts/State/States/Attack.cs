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
        var colliders = Physics.OverlapSphere(owner.transform.position, 2);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == owner.gameObject || collider.gameObject.CompareTag(owner.gameObject.tag)) continue;
            if (collider.gameObject.TryGetComponent<StateAgent>(out var component))
            {
                if (component.health.value <= 0)
                {
                    owner.timer.value = 1;
                    return;
                }
            }
        }

        owner.navigation.targetNode = null;
        owner.agentMovement.Stop();
        owner.animationDone.value = false;
        owner.animator.SetTrigger("attack");

        AnimationClip[] clips = owner.animator.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.FirstOrDefault<AnimationClip>(clip => clip.name == "SillyAttack");

        owner.timer.value = (clip != null) ? clip.length : 1;
        
        foreach (var collider in colliders)
        {
            if (collider.gameObject == owner.gameObject || collider.gameObject.CompareTag(owner.gameObject.tag)) continue;
            if (collider.gameObject.TryGetComponent<StateAgent>(out var component))
            {
                component.health.value -= Random.Range(1, 5);
            }
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {

    }
}
