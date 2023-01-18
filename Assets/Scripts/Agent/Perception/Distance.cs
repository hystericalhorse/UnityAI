using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : View
{
    public override GameObject[] getGameObjects()
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
