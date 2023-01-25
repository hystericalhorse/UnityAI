using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PathEdit : MonoBehaviour
{
	[SerializeField] GameObject node_prefab;
	[SerializeField] LayerMask layer_mask;

	private Vector3 position = Vector3.zero;
	private Vector3 node_camera_dir = Vector3.zero;
	private Vector3 active_camera_dir = Vector3.zero;
	private bool spawnable = false;
	private Node node = null;
	private Node active_node = null;

	private void OnEnable()
	{
		if (!Application.isEditor)
		{
			Destroy(this);
		}
		SceneView.duringSceneGui += OnScene;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;
	}

	void OnScene(SceneView scene)
	{
		Event e = Event.current;

		// get scene mouse position
		Vector3 mousePosition = e.mousePosition;
		mousePosition.y = scene.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
		mousePosition.x *= EditorGUIUtility.pixelsPerPoint;

		Ray ray = scene.camera.ScreenPointToRay(mousePosition);
		// check mouse over spawn/nav layer
		if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layer_mask))
		{
			position = hitInfo.point;

			if (hitInfo.collider.gameObject.TryGetComponent<Node>(out node))
			{
				if (active_node == null)
				{
					Selection.activeGameObject = node.gameObject;
				}
				spawnable = false;
			}
			else spawnable = true;
		}
		else
		{
			node = null;
			spawnable = false;
		}

		// if spawnable and mouse pressed, create nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z)
		{
			if (spawnable && node == null && active_node == null) Instantiate(node_prefab, position, Quaternion.identity, transform);
			if (node != null && active_node == null)
			{
				active_node = node;
				node = null;
			}
		}
		// add connection to active nav node
		if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Z)
		{
			if (active_node != null && node != null && active_node != node)
			{
				if (!active_node.neighbors.Exists(n => n == node))
				{
					active_node.neighbors.Add(node);
				}
			}
			active_node = null;
		}

		// delete nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.X)
		{
			if (node != null)
			{
				DestroyImmediate(node.gameObject);
			}
		}

		active_camera_dir = (active_node != null) ? (scene.camera.transform.position - active_node.transform.position).normalized : Vector3.zero;
		node_camera_dir = (node != null) ? (scene.camera.transform.position - node.transform.position).normalized : Vector3.zero;
	}

	private void OnDrawGizmos()
	{
		if (spawnable && node == null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(position, 1);
		}
		if (node != null && node != active_node)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(node.gameObject.transform.position, node.radius);
		}
		if (active_node != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(active_node.gameObject.transform.position, active_node.radius * 1.5f);
			Gizmos.DrawLine(active_node.gameObject.transform.position, position);
		}

	}
}
