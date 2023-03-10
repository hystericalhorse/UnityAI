using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;

public static class Path
{
	public static bool Dijkstra(Node start, Node end, ref List<Node> path)
	{

		// create priority queue
		var nodes = new SimplePriorityQueue<Node>();

		// set source cost to 0
		start.cost = 0;
		// enqueue source node with the source cost as the priority
		nodes.EnqueueWithoutDuplicates(start, start.cost);

		// update until found or no nodes in queue
		bool found = false;
		while (!found && nodes.Count > 0)
		{
			// dequeue node
			var node = nodes.Dequeue();

			// check if node is the destination node
			if (node == end)
			{
				// set found to true
				found = true;
				break;
			}

			foreach (var neighbor in node.neighbors)
			{
				// calculate cost to neighbor = node cost + distance to neighbor
				float cost = node.cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
				// if cost < neighbor cost, add to priority queue
				if (cost < neighbor.cost)
				{
					// set neighbor cost to cost
					neighbor.cost = cost;
					// set neighbor parent to node
					neighbor.parent = node;
					// enqueue without duplicates neighbor with cost as priority
					nodes.EnqueueWithoutDuplicates(neighbor, neighbor.cost);
				}
			}
		}

		if (found)
		{
			// create path from destination to source using node parents
			path = new List<Node>();
			CreatePathFromParents(end, ref path);
		}
		else
		{
			path = nodes.ToList();
		}

		return found;
	}

	public static bool aStar(Node start, Node end, ref List<Node> path)
	{

		// create priority queue
		var nodes = new SimplePriorityQueue<Node>();

		// set source cost to 0
		start.cost = 0;
		float h = Vector3.Distance(start.transform.position, end.transform.position);
		// enqueue source node with the source cost as the priority
		nodes.EnqueueWithoutDuplicates(start, start.cost + h);

		// update until found or no nodes in queue
		bool found = false;
		while (!found && nodes.Count > 0)
		{
			// dequeue node
			var node = nodes.Dequeue();

			// check if node is the destination node
			if (node == end)
			{
				// set found to true
				found = true;
				break;
			}

			foreach (var neighbor in node.neighbors)
			{
				// calculate cost to neighbor = node cost + distance to neighbor
				float cost = node.cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
				// if cost < neighbor cost, add to priority queue
				if (cost < neighbor.cost)
				{
					// set neighbor cost to cost
					neighbor.cost = cost;
					// set neighbor parent to node
					neighbor.parent = node;
					h = Vector3.Distance(start.transform.position, end.transform.position);
					// enqueue without duplicates neighbor with cost as priority
					nodes.EnqueueWithoutDuplicates(neighbor, neighbor.cost + h);
				}
			}
		}

		if (found)
		{
			// create path from destination to source using node parents
			path = new List<Node>();
			CreatePathFromParents(end, ref path);
		}
		else
		{
			path = nodes.ToList();
		}

		return found;
	}

	public static void CreatePathFromParents(Node node, ref List<Node> path)
	{
		// while node not null
		while (node != null)
		{
			// add node to list path
			path.Add(node);
			// set node to node parent
			node = node.parent;
		}

		// reverse path
		path.Reverse();
	}

}
