using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Spell))]
public class SpellEditor : Editor
{
    public override void OnInspectorGUI()
    { 
        Spell spell = (Spell) target;
        spell.spellName = EditorGUILayout.TextField("Spell Name", spell.spellName);
        spell.icon = (Sprite)EditorGUILayout.ObjectField("Item Icon", spell.icon, typeof(Sprite), true);
        spell.spellType = (SpellType)EditorGUILayout.EnumPopup("Spell Type", spell.spellType);
        switch(spell.spellType)
        {
            case SpellType.SelfCast:
                spell.instantCast = EditorGUILayout.Toggle("Instant cast",spell.instantCast);
                if(!spell.instantCast)
                {
                    spell.castTime = EditorGUILayout.FloatField("Cast Time", spell.castTime);
                }
                spell.selfCastRadius = EditorGUILayout.FloatField("Self Cast Radius", spell.selfCastRadius);
                break;
            case SpellType.SkillShot:
                spell.instantCast = EditorGUILayout.Toggle("Instant cast", spell.instantCast);
                if (!spell.instantCast)
                {
                    spell.castTime = EditorGUILayout.FloatField("Cast Time", spell.castTime);
                }
                spell.range = EditorGUILayout.FloatField("Range", spell.range);
                break;
            case SpellType.Targeted:
                spell.instantCast = EditorGUILayout.Toggle("Instant cast", spell.instantCast);
                if (!spell.instantCast)
                {
                    spell.castTime = EditorGUILayout.FloatField("Cast Time", spell.castTime);
                }
                spell.range = EditorGUILayout.FloatField("Range", spell.range);
                spell.targetedRadius = EditorGUILayout.FloatField("Radius", spell.targetedRadius);
                break;

        }
        spell.spellProjectile = (SpellProjectile)EditorGUILayout.EnumPopup("Spell Projectile", spell.spellProjectile);
        switch (spell.spellProjectile)
        {
            case SpellProjectile.Projectile:
                spell.projectile = (GameObject) EditorGUILayout.ObjectField("Projectile", spell.projectile, typeof(GameObject), true);
                spell.travelSpeed = EditorGUILayout.FloatField("Travel Speed", spell.travelSpeed);
                spell.duration = EditorGUILayout.FloatField("Duration", spell.duration);
                break;
            case SpellProjectile.Instant:
                break;
            case SpellProjectile.Constant:
                spell.duration = EditorGUILayout.FloatField("Duration", spell.duration);
                break;
        }
        spell.spellEffect = (SpellEffects)EditorGUILayout.EnumPopup("Spell Effect", spell.spellEffect);
        switch (spell.spellEffect)
        {
            case SpellEffects.Buff:
                break;
            case SpellEffects.Damage:
                spell.damage = EditorGUILayout.FloatField("Damage", spell.damage);
                break;
            case SpellEffects.DamageAndDot:
                spell.initialDamage = EditorGUILayout.FloatField("Initial Damage", spell.initialDamage);
                spell.dotTotalDamage = EditorGUILayout.FloatField("Total Dot Damage", spell.dotTotalDamage);
                spell.dotTickSpeed = EditorGUILayout.FloatField("Dot Tick Speed",spell.dotTickSpeed);
                break;
            case SpellEffects.DamageOverTime:
                spell.dotTotalDamage = EditorGUILayout.FloatField("Total Dot Damage", spell.dotTotalDamage);
                spell.dotTickSpeed = EditorGUILayout.FloatField("Dot Tick Speed", spell.dotTickSpeed);
                break;
            case SpellEffects.Heal:
                spell.healingDone = EditorGUILayout.FloatField("Healing Done", spell.healingDone);
                break;
        }
    }
}
