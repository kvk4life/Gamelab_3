using UnityEngine;
using System.Collections;

public class ProjectileObj : MonoBehaviour
{
    public float damage;
    public float travelSpeed;
    public float duration;
    public float initalDamage;
    public float damageOverTime;
    public float tickSpeed;
    public SpellEffects effect;

    void Awake()
    {
        StartCoroutine("Duration");
    }

    void FixedUpdate()
    {
        transform.position+=transform.forward*travelSpeed*Time.fixedDeltaTime;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
