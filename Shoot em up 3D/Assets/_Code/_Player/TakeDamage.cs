using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private bool _invulnerable;
    [SerializeField] private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_invulnerable)
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                _invulnerable = false;
                _timer = 0f;
            }
        }
    }
    public void DoDamage(float damage)
    {
        if (_invulnerable == false)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Debug.Log("Mori");
            }
            _invulnerable = true;
            _timer = 0;
        }
    }
}
