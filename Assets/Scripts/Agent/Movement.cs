using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 vel { get; set; } = Vector3.zero;
    public Vector3 acc { get; set; } = Vector3.zero;

    [Range(1, 10)] public float max_speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void applyForce(Vector3 F)
    {
        acc += F;
    }

    void LateUpdate()
    {
        vel += acc * Time.deltaTime;
        vel = Vector3.ClampMagnitude(vel, max_speed);
        transform.position += vel * Time.deltaTime;
        if (vel.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(vel);
        }

        acc = Vector3.zero;
    }
}
