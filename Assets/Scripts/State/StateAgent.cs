using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class StateAgent : Agent
{
    [SerializeField] public StateMachine machine = new();
    public GameObject[] seen;

    public Camera mainCamera;

    public FloatRef health = new FloatRef();
    public FloatRef timer = new FloatRef();
    public FloatRef enemyDistance = new FloatRef(); 
    public BoolRef enemySeen = new BoolRef();
    public BoolRef isAlive = new BoolRef();
    public BoolRef animationDone = new BoolRef();
    public BoolRef atDestination = new BoolRef();

    void Start()
    {

        mainCamera = Camera.main;
        machine.AddState(new Death(this));
        machine.AddState(new Idle(this));
        machine.AddState(new Chase(this));
        machine.AddState(new Patrol(this));
        machine.AddState(new Wander(this));
        machine.AddState(new Attack(this));
        machine.AddState(new Flee(this));

        Condition timerExpiredCondition = new FloatCondition(timer, Condition.Predicate.LESS_EQUAL, 0);
        Condition enemySeenCondition = new BoolCondition(enemySeen, true);
        Condition enemyNotSeenCondition = new BoolCondition(enemySeen, false);
        Condition healthLowCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 5);
        Condition healthOkCondition = new FloatCondition(health, Condition.Predicate.GREATER, 5);
        Condition deathCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0);
        Condition animationDoneCondition = new BoolCondition(animationDone, true);
        Condition atDestinationCondition = new BoolCondition(atDestination, true);

        machine.AddTransition(nameof(Idle), new Transition(new Condition[] { timerExpiredCondition }), nameof(Patrol));
        machine.AddTransition(nameof(Idle), new Transition(new Condition[] { healthOkCondition, enemySeenCondition }), nameof(Chase));

        machine.AddTransition(nameof(Patrol), new Transition(new Condition[] { healthOkCondition, enemySeenCondition }), nameof(Chase));
        machine.AddTransition(nameof(Patrol), new Transition(new Condition[] { timerExpiredCondition }), nameof(Wander));

        machine.AddTransition(nameof(Chase), new Transition(new Condition[] { atDestinationCondition, timerExpiredCondition }), nameof(Attack));
        machine.AddTransition(nameof(Chase), new Transition(new Condition[] { enemyNotSeenCondition, timerExpiredCondition }), nameof(Wander));

        machine.AddTransition(nameof(Wander), new Transition(new Condition[] { atDestinationCondition }), nameof(Idle));
        machine.AddTransition(nameof(Wander), new Transition(new Condition[] { atDestinationCondition, enemySeenCondition }), nameof(Attack));

        machine.AddTransition(nameof(Attack), new Transition(new Condition[] { timerExpiredCondition }), nameof(Chase));

        machine.AddTransition(nameof(Chase), new Transition(new Condition[] { enemyNotSeenCondition , timerExpiredCondition }), nameof(Idle));

        machine.AddTransition(nameof(Flee), new Transition(new Condition[] { healthLowCondition, enemyNotSeenCondition }), nameof(Idle));

        machine.AddAnyTransition(new Transition(new Condition[] { healthLowCondition, enemySeenCondition }), nameof(Flee));
        machine.AddAnyTransition(new Transition(new Condition[] { deathCondition }), nameof(Death));

        machine.StartState(nameof(Idle));
    }
    
    void Update()
    {
        seen = agentView.getGameObjects();

        enemySeen.value = (seen.Length != 0);
        enemyDistance.value = (enemySeen) ? (Vector3.Distance(transform.position, seen[0].transform.position)) : float.MaxValue;
        timer.value -= Time.deltaTime;
        atDestination.value = ((agentMovement.destination - transform.position).sqrMagnitude <= 1);
        animationDone.value = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));

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
