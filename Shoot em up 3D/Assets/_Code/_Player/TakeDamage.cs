using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_health);
    }
    public void DoDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Debug.Log("Mori");
        }
    }
}
