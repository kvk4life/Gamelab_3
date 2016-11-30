using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    private MinionStats stats;
    private float dist;
    public float distance, timeBetweenAttacks;
    private bool reCheck, controllPoint, attack;
    public GameObject controllObject;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Champion");
        unitClass = GetComponent<Unit>();
        stats = GetComponent<MinionStats>();
	}
	
	void Update () {
        CheckDistance();
	}

    void CheckDistance() { // checks for the distance beteen the player and the enemy
        if (!controllPoint) {
            dist = Vector3.Distance(player.transform.position, transform.position);
        }
        else {
            dist = Vector3.Distance(controllObject.transform.position, transform.position);
        }

        if (dist <= distance) {
            if (!attack) {
                StartCoroutine(Attack());
                reCheck = true;
                unitClass.StopCoroutine(unitClass.FollowPath());
                attack = true;
            }
        }
        else {
            attack = false; ;
            StopCoroutine(Attack());
            if (reCheck) {
                if (controllPoint) {
                    unitClass.target = controllObject.transform;
                }
                else {
                    unitClass.target = player.transform;
                }

               // PathRequestManager.RequestPath(transform.position, unitClass.target.position, unitClass.OnPathFound, unitClass.flyable);
                reCheck = false;
            }
        }

    }

    IEnumerator Attack() {
        //damage method for player
        print("hit");
        yield return new WaitForSeconds(timeBetweenAttacks);
        StartCoroutine(Attack());
    }
}
