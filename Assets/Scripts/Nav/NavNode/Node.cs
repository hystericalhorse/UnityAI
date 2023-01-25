using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
	[SerializeField] public List<Node> neighbors = new List<Node>();
	[SerializeField, Range(1, 10)] public float radius = 1f;

	public Node parent { get; set; } = null;
	public bool visited { get; set; } = false;
	public float cost { get; set; } = float.MaxValue;

	private void OnValidate()
	{
		GetComponent<SphereCollider>().radius = radius;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, radius);

		Gizmos.color = Color.green;
		foreach (var node in neighbors)
		{
			Gizmos.DrawLine(transform.position, node.transform.position);
		}
	}

	public static void ShowNodes()
	{
		var nodes = GetNodes();
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = true);
	}

	public static void HideNodes()
	{
		var nodes = GetNodes();
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = false);
	}

	public static void ResetNodes()
	{
		var nodes = GetNodes();
		nodes.ToList().ForEach(node =>
		{
			node.visited = false;
			node.parent = null;
			node.cost = float.MaxValue;
		});
	}

	public static Node[] GetNodes()
	{
		return FindObjectsOfType<Node>();
	}

	public static Node GetRandomNode()
	{
		var nodes = GetNodes();
		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];
	}
}
