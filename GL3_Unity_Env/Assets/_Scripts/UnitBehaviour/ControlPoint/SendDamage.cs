using UnityEngine;
using System.Collections;

public class SendDamage : MonoBehaviour {

    public void HealthChecker(int sendDamage) {
        GetComponentInParent<ControlPoint>().HealthChecker(sendDamage);
    }
}
