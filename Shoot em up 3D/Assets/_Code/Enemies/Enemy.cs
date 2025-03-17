using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;
    public float health;
    public float travelDistance;

    [SerializeField] private NavMeshAgent agent;

    public void Start()
    {
        Debug.Log("Got agent Perry el ornitorrinco" + agent.hasPath);
        SetUpStateMachine();
    }
    public void Update()
    {
        stateMachine.Update();
    }
    public void EnteringWanderingState()
    {

    }
    public void UpdatingWanderingState()
    {
        var randomOffset = Random.insideUnitCircle * travelDistance;
        agent.SetDestination(transform.position + new Vector3(randomOffset.x+10, 0, randomOffset.y+10));
    }
    public void ExitingWanderingState()
    {

    }
    public void EnteringAttackState()
    {

    }
    public void UpdatingAttackState()
    {

    }
    public void ExitingAttackState()
    {

    }
    public void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
        var wanderingState = new WanderingState(this);
        var attackState = new AttackState(this);
        stateMachine.AddTransition(wanderingState, attackState, new FuncPredicate(() => health <= 0));
        stateMachine.SetState(wanderingState);
    }
}
