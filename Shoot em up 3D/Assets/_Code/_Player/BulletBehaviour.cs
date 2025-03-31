using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] protected WeaponInfo _weaponInfo;
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
            damageable.DoDamage(_weaponInfo.weaponDamage);
            _weaponController.ReturnBulelt(gameObject);
        }
    }
}
