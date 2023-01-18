using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidance : MonoBehaviour
{
    public Transform raycast_transform;
    [Range(1, 40)] public float distance = 1;
    [Range(0, 180)] public float max_angle = 45;
    [Range(2, 50)] public int raycasts = 2;

    public LayerMask layerMask;

    public bool isObstancleBlocking()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 2, distance, layerMask);
    }

    public Vector3 getOpenDirection()
    {
        Vector3[] directions = Utilities.getCirclularDirections(raycasts, (int) max_angle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(raycast_transform.position, raycast_transform.rotation * direction);

            if (Physics.SphereCast(ray, 2, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan);
                return ray.direction;
            }
        }

        return transform.forward;
    }
}
