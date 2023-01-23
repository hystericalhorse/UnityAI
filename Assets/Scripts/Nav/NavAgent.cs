using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : Agent
{
	[SerializeField] Node start_node;

	public Node target_node { get; set; }

	void Start()
	{
		target_node = (start_node != null) ? start_node : Node.GetRandomNode();
	}

	void Update()
	{
		if (target_node != null)
		{
			agentMovement.moveTowards(target_node.transform.position);
		}
	}
}
