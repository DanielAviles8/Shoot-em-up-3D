using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private WeaponInfo _weaponInfo;
    // Start is called before the first frame update
    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log("Applying damage: " + _weaponInfo.weaponDamage);
            damageable.DoDamage(_weaponInfo.weaponDamage);
        }
    }
}
