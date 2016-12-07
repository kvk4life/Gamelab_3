using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public void FireProjectile(Transform firePoint, GameObject projectile, float damage, float travelSpeed, float duration, float initalDamage, float damageOverTime, float tickSpeed, SpellEffects effect)
    {
        print("Shot2");
        ProjectileObj obj = projectile.GetComponent<ProjectileObj>();
        obj.duration=duration;
        obj.travelSpeed=travelSpeed;
        obj.damage=damage;
        obj.initalDamage=initalDamage;
        obj.tickSpeed=tickSpeed;
        obj.effect=effect;
        obj.damageOverTime=damageOverTime;
        Instantiate(projectile,firePoint.position,firePoint.rotation);
    }
}
