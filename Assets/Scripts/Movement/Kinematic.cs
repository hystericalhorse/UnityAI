using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : Movement
{
	public override void Stop()
	{
		vel = Vector3.zero;
	}

	public override void applyForce(Vector3 F)
    {
        acc += F;
    }

	public override void moveTowards(Vector3 target)
	{
		Vector3 direction = (target - transform.position).normalized;
		applyForce(direction * max_force);
	}

    public override void Resume()
    {

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
