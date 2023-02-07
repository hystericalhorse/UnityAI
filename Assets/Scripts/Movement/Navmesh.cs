using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navmesh : Movement
{
	[SerializeField] NavMeshAgent navmeshAgent;

	public override Vector3 vel
	{
		get => navmeshAgent.velocity;
		set => navmeshAgent.velocity = value;
	}

	public override Vector3 destination
	{
		get => navmeshAgent.destination;
		set => navmeshAgent.destination = value;
	}

	void LateUpdate()
	{
		navmeshAgent.speed = max_speed;
		navmeshAgent.acceleration = max_force;
		navmeshAgent.angularSpeed = turn_rate;
	}

	public override void applyForce(Vector3 force)
	{
		//
	}

	public override void moveTowards(Vector3 target)
	{
		navmeshAgent.SetDestination(target);
	}

	public override void Resume()
	{
		navmeshAgent.isStopped = false;
	}

	public override void Stop()
	{
		navmeshAgent.isStopped = true;
	}
}
