using UnityEngine;

public class EnemyBulletBehaviour : BulletBehaviour
{
    protected override void OnTriggerEnter(Collider collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.DoDamage(_weaponInfo.weaponDamage);
        }
    }
}
