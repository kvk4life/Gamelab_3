using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    private MinionStats stats;
    private float dist;
    public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd;
    private bool reCheck, controllPoint, attack;
    public GameObject controllObject;
    private Vector3 myTarget;


    void Start () {
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        unitClass = GetComponent<Unit>();
        stats = GetComponent<MinionStats>();
        StartCoroutine(ThinkAboutNextAction());
    }
	
	void Update () {
        CheckDistance();
	}

    public void EndLife() {
        StopCoroutine(ThinkAboutNextAction());
    }

    IEnumerator ThinkAboutNextAction() {
        DecideTarget();
        yield return new WaitForSeconds(thinkingSpd);
        StartCoroutine(ThinkAboutNextAction());
    }

    void DecideTarget() {
        float decideTarget = Random.Range(0, 100);
        myTarget = (decideTarget < myTargetChance) ? player.transform.position : controllObject.transform.position;
    }

    void CheckDistance() { // checks for the distance beteen the player and the enemy
        if (controllPoint) {
            DecideTarget();
        }
        dist = Vector3.Distance(myTarget, transform.position);
        /*
        if (!controllPoint) {
            dist = Vector3.Distance(player.transform.position, transform.position);
        }
        else {
            dist = Vector3.Distance(controllObject.transform.position, transform.position);
        }
        */
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
