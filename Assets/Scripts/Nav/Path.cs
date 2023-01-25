using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;

public static class Path
{
	public static bool Dijkstra(Node start, Node end, ref List<Node> path)
	{
		bool found = false;

		// create priority queue
		var nodes = new SimplePriorityQueue<Node>();

		// set source cost to 0
		start.cost = 0;
		// enqueue source node with the source cost as the priority
		// TODO

		// update until found or no nodes in queue
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
				float cost = 0; // <- TODO
				// if cost < neighbor cost, add to priority queue
				if (cost < neighbor.cost)
				{
					// set neighbor cost to cost
					// TODO
					// set neighbor parent to node
					// TODO
					// enqueue without duplicates neighbor with cost as priority
					// TODO
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
			// TODO
			// set node to node parent
			// TODO
		}

		// reverse path
		path.Reverse();
	}

}
