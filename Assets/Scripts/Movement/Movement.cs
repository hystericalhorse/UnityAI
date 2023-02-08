using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public virtual Vector3 vel { get; set; } = Vector3.zero;
    public virtual Vector3 acc { get; set; } = Vector3.zero;
    public virtual Vector3 dir { get { return vel.normalized; } }
    public virtual Vector3 destination { get; set; } = Vector3.zero;

    [Range(1, 10)] public float min_speed = 1;
    [Range(1, 10)] public float max_speed = 5;
    [Range(1, 100)] public float max_force = 5;
    [Range(0, 720)] public float turn_rate = 90;

    [Range(0, 10)] public float persistence = 1.0f;

    public abstract void Stop();
    public abstract void applyForce(Vector3 F);
    public abstract void moveTowards(Vector3 target);
    public abstract void Resume();
}
