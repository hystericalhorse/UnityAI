using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public float wander_distance = 1f;
    public float wander_radius = 3f;
    public float wander_displacement = 5f;

    public float wander_angle { get; set; } = 0f;

    void Update()
    {
        var objs = this.agentView.getGameObjects();
        foreach (var obj in objs)
        {
            Debug.DrawLine(this.transform.position, obj.transform.position);
        }

        if (objs.Length > 0) 
        {
            agentMovement.applyForce(Steering.Seek(this, objs[0]) * 0.25f);
            agentMovement.applyForce(Steering.Flee(this, objs[0]) * 0.75f);
        }

        if (agentMovement.acc.sqrMagnitude <= agentMovement.max_force * 0.1f)
        {
            agentMovement.applyForce(Steering.Wander(this));
        }

        transform.position = Utilities.WrapWorld(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }


}
