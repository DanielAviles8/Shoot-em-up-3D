using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private WaveSpawner waveSpawner;
    public StateMachine stateMachine;
    public float health;
    public static bool death;

    [Header("Wandering")]
    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1.0f;

    private Vector3 _targetPos;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject Player;

    [Header("Attacking")]
    public GameObject EnemyBulletPrefab;
    [SerializeField] private float _timer;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _exitTimer;
    [SerializeField] private float _timeTillExit;
    [SerializeField] private float _distanceToCountExit;
    [SerializeField] private float _minDistance;
    [SerializeField] private Transform _firePoint;
    private bool attack;
 

    public void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
        Player = GameObject.FindGameObjectWithTag("Player");
        SetUpStateMachine();
        death = false;

    }
    public void Update()
    {
        stateMachine.Update();
    }
    public void EnteringWanderingState()
    {
        _targetPos = GetRandomPointInCircle();
        agent.SetDestination(_targetPos);
    }
    public void UpdatingWanderingState()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            _targetPos = GetRandomPointInCircle();
            agent.SetDestination(_targetPos);
        }
    }
    public void ExitingWanderingState()
    {

    }
    public void EnteringChasingState()
    {
        agent.SetDestination(Player.transform.position);
    }
    public void UpdatingChasingState()
    {
        if (Player != null)
        {
            agent.SetDestination(Player.transform.position);
        }

    }
    public void ExitingChasingState()
    {

    }
    public void EnteringAttackState()
    {
        attack = true;
    }
    public void UpdatingAttackState()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 lookDirection = (Player.transform.position - transform.position).normalized;
        lookDirection.y = 0; 
        transform.rotation = Quaternion.LookRotation(lookDirection);

        if (distance < _minDistance)
        {
            Vector3 direction = transform.position - Player.transform.position;
            direction.y = 0;
            direction.Normalize();

            Vector3 targetPosition = Player.transform.position + direction * _minDistance;

            if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        if (_timer > _timeBetweenShots)
        {
            _timer = 0;
            GameObject newBullet = Instantiate(EnemyBulletPrefab, _firePoint.position, _firePoint.rotation);
        }
        if (Vector2.Distance(Player.transform.position, agent.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if(_exitTimer > _timeTillExit)
            {
                attack = false;
            }
        }
        else
        {
            _exitTimer = 0;
        }
        _timer += Time.deltaTime;
    }
    public void ExitingAttackState()
    {

    }
    public void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemigo muerto");
            death = true;
            gameObject.SetActive(false);
        }
    }
    private Vector3 GetRandomPointInCircle()
    {
        return agent.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
    private void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
        var wanderingState = new WanderingState(this);
        var attackState = new AttackState(this);
        var chasingState = new ChasingState(this);
        stateMachine.AddTransition(wanderingState, chasingState, new FuncPredicate(() => ChaseTrigger.Chase == true));
        stateMachine.AddTransition(chasingState, wanderingState, new FuncPredicate(() => ChaseTrigger.Chase == false));
        stateMachine.AddTransition(chasingState, attackState, new FuncPredicate(() => AttackTrigger.Attack == true));
        stateMachine.AddTransition(attackState, chasingState, new FuncPredicate(() => attack == false));
        stateMachine.SetState(wanderingState);

    }
}
