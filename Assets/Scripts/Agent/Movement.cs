using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 vel { get; set; } = Vector3.zero;
    public Vector3 acc { get; set; } = Vector3.zero;

    [Range(1, 10)] public float min_speed = 1;
    [Range(1, 10)] public float max_speed = 5;
    [Range(1, 100)] public float max_force = 5;

    [Range(0, 10)] public float persistence = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void Stop()
	{
		vel = Vector3.zero;
	}

	public void applyForce(Vector3 F)
    {
        acc += F;
    }

	public void moveTowards(Vector3 target)
	{
		Vector3 direction = (target - transform.position).normalized;
		applyForce(direction * max_force);
	}

	void LateUpdate()
    {
        vel += acc * Time.deltaTime;
        vel = Utilities.ClampMagnitude(vel, min_speed, max_speed);
        transform.position += vel * Time.deltaTime;
        if (vel.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(vel);
        }

        acc = Vector3.zero;
    }
}
