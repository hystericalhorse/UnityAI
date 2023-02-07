using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    [SerializeField] public StateMachine machine = new();
    public GameObject[] seen;

    public Camera mainCamera;

    void Start()
    {

        mainCamera = Camera.main;
        machine.AddState(new Idle(this));
        machine.AddState(new Chase(this));
        machine.AddState(new Patrol(this));
        machine.AddState(new Wander(this));
        machine.AddState(new Attack(this));

        machine.StartState(nameof(Idle));
    }
    
    void Update()
    {
        seen = agentView.getGameObjects();

        machine.Update();

        if (navigation.targetNode != null)
        {
            agentMovement.moveTowards(navigation.targetNode.transform.position);
        }

        animator.SetFloat("velocity", agentMovement.vel.magnitude);
    }

    private void OnGUI()
    {
        Vector3 point = mainCamera.WorldToScreenPoint(transform.position);
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        GUI.Label(rect, machine.currentState.name);
    }
}
