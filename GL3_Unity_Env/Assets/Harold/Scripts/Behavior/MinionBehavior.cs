﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

	public int moveSpeed;
	public float timeBetweenAttacks, attackRange;
	public string[] taggList;
	public GameObject spawner;
	private MinionSpawner spawnScript;
	[SerializeField]
	private Transform curretTarget;
	private int counter, resetMoveSpeed;
	public int teamNumber; //tijdelijk om te checcken voor allie of enemy
	public MinionStats statsClass;
	private bool attackOff;

	private List<Transform> wayPointList = new List<Transform>();
	private List<GameObject> enemyList = new List<GameObject>();
	private List<GameObject> allyList = new List<GameObject>();


	void Start () {
		spawnScript = spawner.GetComponent<MinionSpawner>();
		statsClass = GetComponent<MinionStats>();
		attackOff = true;
		AddWayPoints();
		curretTarget = wayPointList[counter];
		resetMoveSpeed = moveSpeed;
	}

	void Update () {
		transform.LookAt(curretTarget.position);
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

		if(enemyList.Count > 0){
			DistanceChecker();
		}
	}

	public void Damaged(int Damage, bool magic){
		//armour or magic ressist retract
		statsClass.health-= Damage;

		if(statsClass.health <= 0){
			Destroy(gameObject);
		}
	}

	void DistanceChecker(){
		var distance = Vector3.Distance(transform.position, curretTarget.position);

		if(distance <= attackRange){
			moveSpeed = 0;
		}
		else{
			moveSpeed = resetMoveSpeed;
		}
	}

	void CheckTarget(){
		if(enemyList.Count > 0){
			curretTarget = enemyList[0].transform;
			if(attackOff){
				StartCoroutine(Attacking(timeBetweenAttacks));
				attackOff = false;
			}
		}
		else{
			curretTarget = wayPointList[counter];
			StopCoroutine("Attacking");
			attackOff = true;
		}
	}

	IEnumerator Attacking(float attackTime) {
		if(curretTarget.tag == taggList[0]){
		}
		yield return new WaitForSeconds(attackTime);
		StartCoroutine(Attacking(timeBetweenAttacks));
		//curretTarget.
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
			curretTarget = wayPointList[counter];
		}

		if(trigger.gameObject.GetComponent<MinionBehavior>() != null){
			AgroAdd(trigger.gameObject);
		}

	}

	void OnTriggerExit(Collider trigger){
		if(trigger.GetComponent<MinionBehavior>() != null){
			AgroRemove(trigger.gameObject);
		}

	}

	void AgroRemove(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.gameObject.GetComponent<MinionBehavior>().teamNumber != teamNumber){
					enemyList.Remove(target);
				}
				else{
					allyList.Remove(target);
					break;
				}
			}
		}
		CheckTarget();
	}

	void AgroAdd(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.gameObject.GetComponent<MinionBehavior>().teamNumber != teamNumber){
					enemyList.Add(target);
					print("enemyAdded");
				}
				else{
					allyList.Add(target);
					break;
				}
				//curretTarget = target.transform;

			}
		}
		CheckTarget();
	}
}
