using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavAgent : Agent
{
	[SerializeField] Node start_node;
	[SerializeField] Node end_node;
	[SerializeField] NavPath nav_path;

	public Node target_node { get; set; }

	void Start()
	{
		if (nav_path == null) target_node = (start_node != null) ? start_node : Node.GetRandomNode();
		else
		{
			nav_path.startNode = (start_node != null) ? start_node : GetNearestNode();
			nav_path.endNode = (end_node != null) ? end_node : Node.GetRandomNode();
			nav_path.StartPath();
			target_node = nav_path.startNode;
		}
	}

	void Update()
	{
		if (target_node != null)
		{
			agentMovement.moveTowards(target_node.transform.position);
		}
		else
		{
			agentMovement.Stop();
		}
	}

	public Node GetNextTarget(Node node)
	{
		if (nav_path == null) return node.neighbors[Random.Range(0, node.neighbors.Count)];
		else return nav_path.GetNextNode(node);
	}

	public Node GetNearestNode()
	{
		var nodes = Node.GetNodes().ToList();
		SortByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

	public void SetDestination(Node destinationNode)
	{
		nav_path.startNode = GetNearestNode();
		nav_path.endNode = destinationNode;
		nav_path.StartPath();
		target_node = nav_path.startNode;
	}

	public void SortByDistance(List<Node> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(Node a, Node b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
}
