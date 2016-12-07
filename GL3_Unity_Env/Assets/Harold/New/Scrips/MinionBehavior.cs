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
    [HideInInspector]
    public GameObject controllObject;
    private Vector3 myTarget;
    private Coroutine curCoroutine;
    private PBAnim pBAnim;


    void Start () {
        pBAnim = GetComponent<PBAnim>();
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        unitClass = GetComponent<Unit>();
        stats = GetComponent<MinionStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }
	
	void Update () {
        if (Input.GetButtonDown("Jump")) {
            StartCoroutine(Attack());
        }
        //CheckDistance();
	}

    public void EndLife() {
        StopCoroutine(curCoroutine);
    }

    IEnumerator ThinkAboutNextAction() {
        DecideTarget();
        yield return new WaitForSeconds(thinkingSpd);
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    void DecideTarget() {
        float decideTarget = Random.Range(0, 100);
        myTarget = (decideTarget < myTargetChance) ? player.transform.position : controllObject.transform.position;
    }

    void CheckDistance() { // checks for the distance beteen the player and the enemy
        if (controllPoint) {
            DecideTarget();
        }
        else {
            myTarget = player.transform.position;
        }
        dist = Vector3.Distance(myTarget, transform.position);
        /* verwijder de code in deze comment wanneer er gecheckt is dat de terniairy statement net zo goed werkte
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
        //speel de attack animatie af
        pBAnim.Attack();
        print("Attacks");
        yield return new WaitForSeconds(timeBetweenAttacks);
        StartCoroutine(Attack());
    }
}
