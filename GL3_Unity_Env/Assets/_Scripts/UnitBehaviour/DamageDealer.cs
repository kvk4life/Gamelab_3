using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
    public GameObject demon;
    private DemonBehaviour demonBehaviour;
    private GameObject target;
    private int damage;

    public void Start() {
        demonBehaviour = demon.GetComponent<DemonBehaviour>();
    }

    public void OnTriggerEnter(Collider col) {
        target = demonBehaviour.curTar;
        damage = demonBehaviour.damage;
        if (col.transform.tag == target.tag) {
            if (col.transform.tag == "Champion") {
                col.transform.GetComponent<Health>().GetDamage(damage);
            }
            else {
                col.transform.GetComponent<ControlPoint>().HealthChecker(damage);
            }
        }
    }
}
