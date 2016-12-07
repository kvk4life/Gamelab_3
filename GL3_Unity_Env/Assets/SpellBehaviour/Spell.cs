using UnityEngine;
using System.Collections;

    public enum SpellType { SelfCast, SkillShot, Targeted};
    public enum SpellProjectile {  Projectile, Instant, Constant};
    public enum SpellEffects { Damage, Heal, DamageAndDot, DamageOverTime, Buff};
public class Spell : MonoBehaviour
{

    [Header("Spell Type and Ranges")]
    public string spellName;
    public Sprite icon;
    public SpellType spellType;
    public SpellProjectile spellProjectile;
    public GameObject projectile;
    public bool instantCast;
    public float castTime;
    public float range;
    public float selfCastRadius;
    public float targetedRadius;
    public float travelSpeed;
    public float duration;

    [Header("Spell Effects")]
    public SpellEffects spellEffect;
    public float damage;
    public float healingDone;
    public float initialDamage;
    public float dotTotalDamage;
    public float dotTickSpeed;
}
