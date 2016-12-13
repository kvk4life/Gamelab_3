using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    private MinionStats stats;
    public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd;
    private bool reCheck, attack;
    [HideInInspector]
    public GameObject controllObject;
    private Vector3 myTarget;
    private Coroutine curCoroutine;
    private PBAnim pBAnim;
    private ControlPoint controlPoint;
    private Coroutine attackCoroutine;


    void Start () {
        pBAnim = GetComponent<PBAnim>();
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        controlPoint = controllObject.GetComponent<ControlPoint>();
        unitClass = GetComponent<Unit>();
        stats = GetComponent<MinionStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }
	
    public void EndLife() {
        StopCoroutine(curCoroutine);
    }

    IEnumerator ThinkAboutNextAction() {
        CheckDistance();
        yield return new WaitForSeconds(thinkingSpd);
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    void DecideTarget() {
        if (controlPoint.myCrystal.activeSelf) {
            float decideTarget = Random.Range(0, 100);
            myTarget = (decideTarget < myTargetChance) ? player.transform.position : controllObject.transform.position;
        }
        else {
            myTarget = player.transform.position;
        }
    }

    void CheckDistance() { // checks for the distance beteen the player and the enemy
        DecideTarget();
        float dist = Vector3.Distance(myTarget, transform.position);
        if (dist <= distance) {
            if (!attack) {
                attackCoroutine = (StartCoroutine(Attack()));
                reCheck = true;
                //unitClass.StopCoroutine(unitClass.FollowPath());
                attack = true;
            }
        }
        else {
            attack = false; ;
            if (attackCoroutine != null) { 
                StopCoroutine(attackCoroutine);
            };
            if (reCheck) {
                if (controlPoint.myCrystal.activeSelf) {
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
        //speel de attack animatie af
        pBAnim.Attack();
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackCoroutine = (StartCoroutine(Attack()));
    }
}
