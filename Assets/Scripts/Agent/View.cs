using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [Range(1.0f, 50.0f)]  public float distance = 1.0f;
    [Range(0.0f, 180.0f)] public float max_angle = 45.0f; // degrees

    public GameObject[] getGameObjects()
    {
        List<GameObject> objs = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;
            objs.Add(collider.gameObject);
        }

        return objs.ToArray();
    }
}
