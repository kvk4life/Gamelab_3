using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
    public GameObject demon;
    private DemonMakeShiftBs demonBehaviour;
    private GameObject target;
    private int damage;

    public void Start() {
        demonBehaviour = demon.GetComponent<DemonMakeShiftBs>();
    }

    public void OnTriggerEnter(Collider col) {
        target = demonBehaviour.player.gameObject;
        damage = demonBehaviour.damage;
        if (col.transform.tag == target.tag) {
            if (col.transform.tag == "Champion") {
                col.transform.GetComponent<PlayerHealth>().GetDamage(damage);
            }
            else {
                col.transform.GetComponent<ControlPoint>().HealthChecker(damage);
            }
        }
    }
}
