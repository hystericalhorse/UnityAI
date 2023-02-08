using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
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

    public abstract GameObject[] getGameObjects();
}
