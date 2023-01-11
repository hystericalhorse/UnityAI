using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    void Update()
    {
        var objs = this.agentView.getGameObjects();
        foreach (var obj in objs)
        {
            Debug.DrawLine(this.transform.position, obj.transform.position);
        }

        if (objs.Length > 0) 
        {
            Vector3 direction = (objs[0].transform.position - transform.position).normalized;
            agentMovement.applyForce(direction * 2);
        }

        transform.position = Utilities.WrapWorld(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
