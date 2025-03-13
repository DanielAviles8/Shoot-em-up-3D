using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private WeaponInfo _weaponInfo;
    public WeaponController _weaponController
    {
        get;
        set;
    }
    // Start is called before the first frame update
    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.DoDamage(_weaponInfo.weaponDamage);
            Debug.Log(_weaponController.gameObject.name);
            _weaponController.ReturnBulelt(gameObject);
        }
    }
}
