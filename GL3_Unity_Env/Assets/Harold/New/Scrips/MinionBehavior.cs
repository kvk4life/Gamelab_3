using UnityEngine;
using System.Collections;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    public float distance;
    private bool reCheck;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Champion");
        unitClass = GetComponent<Unit>();
	}
	
	void Update () {
        CheckDistance();
	}

    void CheckDistance() {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if(dist <= distance) {
            reCheck = true;
            unitClass.StopCoroutine(unitClass.FollowPath());
        }
        else {
            if (reCheck) {
                unitClass.StartCoroutine(unitClass.FollowPath());
                reCheck = false;
            }
        }

    }
}
