using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NavPath : MonoBehaviour
{
	public Action end_action = Action.RANDOM;
	List<Node> path = new List<Node>();

	public Node startNode { get; set; }
	public Node endNode { get; set; }

	public enum Action
	{
		RANDOM, PINGPONG, STOP
	}

	public void StartPath()
	{
		GeneratePath();
	}

	public Node GetNextNode(Node Node)
	{
		if (path.Count == 0) return null;

		int index = path.FindIndex(node => node == Node);
		// check if noode index is at the end of the path
		if (index == path.Count - 1)
		{
			switch (end_action)
			{
				default:
				case Action.STOP: { return null; }
				case Action.PINGPONG: { SwapStart();  break; }
				case Action.RANDOM: { SetRandomEndNode(); break; }
			}
			
			// generate new path
			GeneratePath();

			index = 0;
		}

		// get the next node using index + 1
		Node nextNode = path[index + 1];

		return nextNode;
	}

	private void SwapStart()
	{
		Node t = startNode;
		startNode = endNode;
		endNode = t;
	}

	private void SetRandomEndNode()
	{
		// set the start node to the current end node
		startNode = endNode;
		// find a new random end node that isn't the start node
		do
		{
			endNode = Node.GetRandomNode();
		} while (startNode == endNode);
	}

	private void GeneratePath()
	{
		Node.ResetNodes();
		Path.Dijkstra(startNode, endNode, ref path);
	}

	private void OnDrawGizmos()
	{
		foreach (Node node in path)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(node.gameObject.transform.position, node.radius);
		}
		if (startNode != null) Gizmos.DrawIcon(startNode.transform.position + Vector3.up, "nav_nodeA.png", true, Color.green);
		if (endNode != null) Gizmos.DrawIcon(endNode.transform.position + Vector3.up, "nav_nodeA.png", true, Color.red);
	}
}
