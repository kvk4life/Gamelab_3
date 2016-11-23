using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    private GameObject player;
    private Unit unitClass;
    public float distance, timeBetweenAttacks;
    private bool reCheck, controllPoint;

    public List<GameObject> controllList = new List<GameObject>();


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
            StartCoroutine(Attack());
            reCheck = true;
            unitClass.StopCoroutine(unitClass.FollowPath());
        }
        else {
            StopCoroutine(Attack());
            if (reCheck) {
                if (controllPoint) {
                    int temp = Random.Range(0, controllList.Count);
                    unitClass.target = controllList[temp].transform;
                }
                else {
                    unitClass.target = player.transform;
                }

                PathRequestManager.RequestPath(transform.position, unitClass.target.position, unitClass.OnPathFound, unitClass.flyable);
                reCheck = false;
            }
        }

    }

    IEnumerator Attack() {
        //damage method for player
        yield return new WaitForSeconds(timeBetweenAttacks);
        StartCoroutine(Attack());
    }
}
