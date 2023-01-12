using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Steering
{
    public static Vector3 Steer(Agent agent, Vector3 direction)
    {
        Vector3 desired = direction.normalized * agent.agentMovement.max_speed;
        Vector3 steer = desired - agent.agentMovement.vel;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.agentMovement.max_force);

        return force;
    }

    public static Vector3 Seek(Agent agent, GameObject target)
    {
        return Steer(agent, (target.transform.position - agent.transform.position));
    }

    public static Vector3 Flee(Agent agent, GameObject target)
    {
        return Steer(agent, (agent.transform.position - target.transform.position));
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        agent.wander_angle = agent.wander_angle + Random.Range(-agent.wander_displacement, agent.wander_displacement);

        Quaternion rotation = Quaternion.AngleAxis(agent.wander_angle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * agent.wander_radius);
        Vector3 forward = agent.transform.forward * agent.wander_distance;

        return Steer(agent, forward + point);
    }
}
