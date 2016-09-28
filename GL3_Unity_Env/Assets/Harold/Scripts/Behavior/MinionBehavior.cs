using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    public int moveSpeed;
    public float timeBetweenAttacks, attackRange;
    private float distance;
    public string[] taggList;
    public GameObject spawner;
    private MinionSpawner spawnScript;
    [SerializeField]
    private Transform curretTarget;
    private int counter, resetMoveSpeed;
    public Stats statsClass;
    private bool attackOff;


    private List<Transform> wayPointList = new List<Transform>();
    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> allyList = new List<GameObject>();


    void Start() {
        spawnScript = spawner.GetComponent<MinionSpawner>();
        statsClass = GetComponent<Stats>();
        attackOff = true;
        AddWayPoints();
        curretTarget = wayPointList[counter];
        resetMoveSpeed = moveSpeed;
    }

    void Update() {

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (statsClass.currentHealth <= 0) {
            Destroy(gameObject);
        }

        if (attackOff == false) {
            if (curretTarget != null) {
                distance = Vector3.Distance(gameObject.transform.position, curretTarget.transform.position);
            }

            if (distance <= attackRange) {
                moveSpeed = 0;
            }
        }
    }

    void CheckTarget() {
     
        if (enemyList.Count > 0) {
            if (attackOff) {
                if (enemyList[0] == null) {
                    enemyList.Remove(null);
                    CheckTarget();
                }
                else {
                    curretTarget = enemyList[0].transform;
                    StartCoroutine(Attacking(timeBetweenAttacks));
                    attackOff = false;
                }
            }
        }
        else {
            
            curretTarget = wayPointList[counter];
        }

       // StopCoroutine(Attacking);
        transform.LookAt(curretTarget.position);
    }

    IEnumerator Attacking(float attackTime) {

        if (curretTarget != null) {
            if (enemyList.Count > 0) {
                if (distance <= attackRange) {
                    curretTarget.GetComponent<Stats>().GetDamage(statsClass.damage);
                    print("BamBang");
                }
                else {
                    print("stopeed");
                    StopCoroutine("Attacking");
                }
            }
        }
        else {

        }

        if(curretTarget == null) {
            attackOff = true;
            moveSpeed = resetMoveSpeed;
            CheckTarget();
            StopCoroutine("Attacking");
        }

        yield return new WaitForSeconds(attackTime);
        StartCoroutine(Attacking(timeBetweenAttacks));
    }

	void AddWayPoints(){
		for(int i = 0; i < spawnScript.waypointList.Length; i++){
			wayPointList.Add(spawnScript.waypointList[i]);
		}
	}

	void OnTriggerEnter(Collider trigger){
		if(trigger.transform == wayPointList[counter]){
			counter++;
			if(counter >= wayPointList.Count){
				counter = 0;
			}
			//curretTarget = wayPointList[counter];
		}

		if(trigger.gameObject.GetComponent<Stats>() != null){
			AgroAdd(trigger.gameObject);
		}
        CheckTarget();
    }

	void OnTriggerExit(Collider trigger){
		if(trigger.GetComponent<Stats>() != null){
			AgroRemove(trigger.gameObject);
		}

	}

	void AgroRemove(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.gameObject.GetComponent<Stats>().teamNumber != statsClass.teamNumber) {
					enemyList.Remove(target);
				}
				else{
					allyList.Remove(target);
					break;
				}
			}
		}
	}

	void AgroAdd(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.gameObject.GetComponent<Stats>().teamNumber != statsClass.teamNumber){
					enemyList.Add(target);
                    print("enemyAdded");
                    break;
                }
				else{
					allyList.Add(target);
					break;
				}
				//curretTarget = target.transform;

			}
		}
	}
}
