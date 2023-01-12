using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [Range(0, 2)] public float flee_weight = 1f;
    [Range(0, 2)] public float seek_weight = 1f;
    [Range(0, 2)] public float flock_weight = 1f;
    [Range(0, 5)] public float flock_radius = 1f;
    [Range(0, 2)] public float separate_weight = 1f;
    [Range(0, 2)] public float align_weight = 1f;


    public float wander_distance = 1f;
    public float wander_radius = 3f;
    public float wander_displacement = 5f;

    public float wander_angle { get; set; } = 0f;

    void Update()
    {
        var objs = this.agentView.getGameObjects();
        foreach (var obj in objs)
        {
            Debug.DrawLine(this.transform.position, obj.transform.position, Color.red);
        }

        if (objs.Length > 0)
        {
            agentMovement.applyForce(Steering.Seek(this, objs[0]) * seek_weight);
            agentMovement.applyForce(Steering.Flee(this, objs[0]) * flee_weight);
        }
        else
        {
            if (agentMovement.acc.sqrMagnitude <= agentMovement.max_force * 0.1f)
            {
                agentMovement.applyForce(Steering.Wander(this));
            }
        }

        if (flockView != null) objs = flockView.getGameObjects();
        if (objs.Length > 0)
        {
            agentMovement.applyForce(Steering.Flock(this, objs) * flock_weight);
            agentMovement.applyForce(Steering.FlockNear(this, objs, flock_radius) * separate_weight);
            agentMovement.applyForce(Steering.FlockAlign(this, objs) * align_weight);
        }

        transform.position = Utilities.WrapWorld(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
