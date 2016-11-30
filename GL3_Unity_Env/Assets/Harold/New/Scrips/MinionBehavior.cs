using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    private MinionStats stats;
    public float distance, timeBetweenAttacks;
    private bool reCheck, controllPoint, attack;

    public List<GameObject> controllList = new List<GameObject>();


	void Start () {
        player = GameObject.FindGameObjectWithTag("Champion");
        unitClass = GetComponent<Unit>();
        stats = GetComponent<MinionStats>();
	}
	
	void Update () {
        CheckDistance();
	}

    void CheckDistance() { // checks for the distance beteen the player and the enemy
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if(dist <= distance) {
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
                    int temp = Random.Range(0, controllList.Count);
                    unitClass.target = controllList[temp].transform;
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
