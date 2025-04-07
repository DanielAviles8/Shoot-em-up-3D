using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TorpedoEnemy : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;
    [SerializeField] private float _health;
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _prefabParticles;

    [Header("Chase")]
    [SerializeField] private NavMeshAgent _agent;
    private GameObject Player;

    [Header("Attacking")]
    [SerializeField] private GameObject EnemyTorpedoBulletPrefab;
    void Start()
    {
        Player = GameObject.Find("Player");
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
            Instantiate(_prefabParticles, transform.position, Quaternion.identity);
            damageable.DoDamage(_weaponInfo.weaponDamage);
            Debug.Log("Choco con player");
            gameObject.SetActive(false);
        }
        
    }
    public void DoDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
