using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spherecast : View
{
    public Transform raycast_transform;
    public float radius = 0.5f;
    [Range(2, 50)] public int raycasts = 2;

    public override GameObject[] getGameObjects()
    {
        List<GameObject> objs = new List<GameObject>();

        Vector3[] directions = Utilities.getCirclularDirections(raycasts, (int) max_angle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(raycast_transform.position, raycast_transform.rotation * direction);
            Debug.DrawRay(ray.origin, raycast_transform.rotation * direction * distance, Color.cyan);

            if (Physics.SphereCast(ray, radius, out RaycastHit hit, distance))
            {
                if (hit.collider.gameObject == gameObject) continue;
                if (tag_name == "" || hit.collider.CompareTag(tag_name))
                {
                    Debug.DrawRay(ray.origin, raycast_transform.rotation * direction * distance, Color.red);
                    objs.Add(hit.collider.gameObject);
                }
            }
        }

        objs = objs.Distinct().ToList();
        objs.Sort(CompareDistance);
        return objs.ToArray();
    }
}
