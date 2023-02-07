using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public View agentView;
    public Movement agentMovement;
    public Animator animator;
    public Navigation navigation;
}
