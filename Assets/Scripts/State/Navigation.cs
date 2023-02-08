using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigation : MonoBehaviour
{
	public Node targetNode { get; set; } = null;


	public Node GetNextNavTarget(Node node)
	{
		return node.neighbors[Random.Range(0, node.neighbors.Count)];
	}

	public Node GetNearestNode()
	{
		var nodes = Node.GetNodes().ToList();
		SortByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

    public Node GetTaggedNode(string tag)
    {
        var nodes = Node.GetNodes(tag).ToList();
        SortByDistance(nodes);

        return (nodes.Count == 0) ? null : nodes[0];
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
