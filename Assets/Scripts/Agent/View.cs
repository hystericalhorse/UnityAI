using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [Range(1.0f, 50.0f)]  public float distance = 1.0f;
    [Range(0.0f, 180.0f)] public float max_angle = 45.0f; // degrees
    public string tag_name = "";

    public int CompareDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

    public GameObject[] getGameObjects()
    {
        List<GameObject> objs = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;
            if (this.tag_name == "" || collider.CompareTag(tag_name))
            {
                Vector3 direction = (collider.transform.position - this.transform.position).normalized;

                /*
                float angle = Vector3.Angle(transform.forward, direction);
                */

                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

                if (angle > max_angle) continue;

                objs.Add(collider.gameObject);
            }

            objs.Sort(CompareDistance);
        }

        return objs.ToArray();
    }
}
