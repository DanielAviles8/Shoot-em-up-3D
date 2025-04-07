using UnityEngine;

public class EnemyBulletBehaviour : BulletBehaviour
{
    protected override void OnTriggerEnter(Collider collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Instantiate(_prefabParticles, transform.position, Quaternion.identity);
            damageable.DoDamage(_weaponInfo.weaponDamage);
            gameObject.SetActive(false);
        }
    }
}
