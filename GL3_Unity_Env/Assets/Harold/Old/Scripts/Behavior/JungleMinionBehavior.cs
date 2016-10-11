using UnityEngine;
using System.Collections;

public class JungleMinionBehavior : MonoBehaviour {

	public JungleMinionStats jungleMinionStats;
    public int minionCampNumber;
    public States creepState;

    public enum States {
        Idle,
        BackToCamp,
        Agro
    }

    void Start () {
		jungleMinionStats = GetComponent<JungleMinionStats>();
	}

	void Update () {

        CheckState();

        if (jungleMinionStats.health <= 0) {
            jungleMinionStats.spawnScript.jungleArray[jungleMinionStats.minionCampNumber].GetComponent<SpawnJungle>().CampMinionDied();
            Destroy(gameObject);
        }
    }

    void CheckState() {
        if(creepState == States.Idle) {
            return;
        }

        if (creepState == States.BackToCamp) {
            return;
        }

        if (creepState == States.Agro) {
            return;
        }
    }
}
