using System.Collections;
using System.Collections.Generic;
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
    }
}
