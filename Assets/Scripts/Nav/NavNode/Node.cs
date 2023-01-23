using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	[SerializeField] public List<Node> neighbors = new List<Node>();
	[SerializeField, Range(1, 10)] public float radius = 1f;

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
