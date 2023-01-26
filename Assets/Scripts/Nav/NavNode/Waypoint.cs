using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Waypoint : Node
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent agent))
		{
			if (agent.target_node == this && neighbors.Count > 0)
			{
				agent.target_node = agent.GetNextTarget(agent.target_node);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent agent))
		{
			if (agent.target_node == this && neighbors.Count > 0)
			{
				agent.target_node = agent.GetNextTarget(agent.target_node);
			}
		}
	}
}
