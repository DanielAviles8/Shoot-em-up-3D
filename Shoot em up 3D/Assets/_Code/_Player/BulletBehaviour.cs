using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] protected WeaponInfo _weaponInfo;
    [SerializeField] public GameObject _prefabParticles;
    public WeaponController _weaponController
    {
        get;
        set;
    }
    private void Update()
    {
        if(this.gameObject.name != "Torpedo")
        {
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        }        
    }
    protected virtual void OnTriggerEnter(Collider collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Instantiate(_prefabParticles, transform.position, Quaternion.identity);
            damageable.DoDamage(_weaponInfo.weaponDamage);
            _weaponController.ReturnBulelt(gameObject);
        }
    }
}
