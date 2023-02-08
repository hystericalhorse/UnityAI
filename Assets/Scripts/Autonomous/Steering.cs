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
        agent.wander_angle += Random.Range(-agent.data.wander_displacement, agent.data.wander_displacement);

        Quaternion rotation = Quaternion.AngleAxis(agent.wander_angle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * agent.data.wander_radius);
        Vector3 forward = agent.transform.forward * agent.data.wander_distance;

        Debug.DrawRay(agent.transform.position, forward + point, Color.magenta);

        return Steer(agent, forward + point);
    }

    public static Vector3 Flock(Agent agent, GameObject[] neighbors)
    {
        Vector3 center = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Length;

        Vector3 force = Steer(agent, center - agent.transform.position);
        Debug.DrawRay(agent.transform.position, force, Color.green);
        return force;
    }

    public static Vector3 FlockNear(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 separate = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            Vector3 direction = agent.transform.position - neighbor.transform.position;
            if (direction.magnitude < radius)
            {
                separate += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = Steer(agent, separate);
        return force;
    }

    public static Vector3 FlockAlign(Agent agent, GameObject[] neighbors)
    {
        Vector3 avg_vel = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            avg_vel = neighbor.GetComponent<Agent>().agentMovement.vel;
        }

        avg_vel /= neighbors.Length;

        Vector3 force = Steer(agent, avg_vel);
        return force;
    }

    public static Vector3 Search(AutonomousAgent agent)
    {
        return Steer(agent, agent.transform.forward);
    }
}
