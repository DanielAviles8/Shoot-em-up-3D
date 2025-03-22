using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TorpedoEnemy : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;
    [SerializeField] private float _health;
    [SerializeField] private WeaponInfo _weaponInfo;

    [Header("Chase")]
    [SerializeField] private NavMeshAgent _agent;
    private GameObject Player;

    [Header("Attacking")]
    [SerializeField] private GameObject EnemyTorpedoBulletPrefab;
    void Start()
    {
        Player = GameObject.Find("Player");
        Debug.Log("Got agent Torpedo" + _agent.hasPath);
        _agent.SetDestination(Player.transform.position);
    }
    void Update()
    {
        if (Player != null) 
        {
            _agent.SetDestination(Player.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.DoDamage(_weaponInfo.weaponDamage);
            Debug.Log("Choco con player");
        }
        
    }
    public void DoDamage(float damage)
    {
        _health = damage;
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
