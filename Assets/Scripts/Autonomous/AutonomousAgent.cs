using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public View flockView;
    public Avoidance obstacleAvoidance;
    public AutonomousAgentData data;

    public float wander_angle { get; set; } = 0f;

    void Update()
    {
        var objs = this.agentView.getGameObjects();
        foreach (var obj in objs)
        {
            Debug.DrawLine(this.transform.position, obj.transform.position, Color.red);
        }

		if (obstacleAvoidance.isObstancleBlocking())
		{
			Vector3 direction = obstacleAvoidance.getOpenDirection();
			agentMovement.applyForce(Steering.Steer(this, direction) * data.obstacle_weight);
		}

		if (objs.Length > 0)
		{
			agentMovement.applyForce(Steering.Seek(this, objs[0]) * data.seek_weight);
			agentMovement.applyForce(Steering.Flee(this, objs[0]) * data.flee_weight);
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
			Vector3 flock_force = Steering.Flock(this, objs) * data.flock_weight;
			Debug.DrawRay(gameObject.transform.position, flock_force, Color.yellow);
			agentMovement.applyForce(flock_force);
			agentMovement.applyForce(Steering.FlockNear(this, objs, data.flock_radius) * data.separate_weight);
			agentMovement.applyForce(Steering.FlockAlign(this, objs) * data.align_weight);
		}
		
        transform.position = Utilities.WrapWorld(transform.position, new Vector3(-50, -50, -50), new Vector3(50, 50, 50));
    }
}
