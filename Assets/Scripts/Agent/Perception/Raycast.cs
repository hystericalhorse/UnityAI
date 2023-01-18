using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : View
{
    public Transform raycast_transform;
    [Range(2, 50)] public int raycasts = 2;

    public override GameObject[] getGameObjects()
    {
        List<GameObject> objs = new List<GameObject>();

        Vector3[] directions = Utilities.getCirclularDirections(raycasts, (int) max_angle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(raycast_transform.position, raycast_transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                if (hit.collider.gameObject == gameObject) continue;
                if (tag_name == "" || hit.collider.CompareTag(tag_name))
                {
                    objs.Add(hit.collider.gameObject);
                }
            }
        }

        return objs.ToArray();
    }
}
