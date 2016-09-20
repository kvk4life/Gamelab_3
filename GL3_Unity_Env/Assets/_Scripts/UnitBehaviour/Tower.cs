﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : Base {
	public enum TowerState
	{
		Hitable,
		Protected,
		Dead,
		ProtectPlayer
	}
	public TowerState myState;
	public TowerState previousState;
	public List<GameObject> targetList = new List<GameObject> ();
	public List<GameObject> allyList = new List<GameObject> ();
	public GameObject[] pooledProjectiles;
	public GameObject enemyPlayer;
	public float attackRate;
	private float nextAttack;

	public void OnTriggerEnter(Collider col){
		if (col.GetComponent<Base> () != null) {
			if (col.GetComponent<Base> ().myTeam != myTeam) {
				//Het object wat door mij trigger veld heen kwam
				//is dus een unit van de enemy team
				targetList.Add (col.gameObject);
				print ("unit geadd aan target list");
			}
			else {
				if (col.transform.tag == "Player") {
					//de methods van "TestPlayer" zullen later wss andere namen krijgen
					GameObject ally = col.gameObject;
					ally.GetComponent<TestPlayer> ().insideTurret = true;
					ally.GetComponent<TestPlayer> ().turretIAmIn = gameObject;
					allyList.Add (ally);
					print ("ally geadd");
				}
			}
		}
	}

	public void OnTriggerExit(Collider col){
		if (col.GetComponent<Base> () != null) {
			if (col.GetComponent<Base> ().myTeam != myTeam) {
				if(enemyPlayer != null && col.gameObject == enemyPlayer){
					enemyPlayer = null;
				}
				for (int e = 0; e < targetList.Count; e++) {
					if (col.gameObject == targetList [e]) {
						targetList.Remove (col.gameObject);
						break;
					}
				}
				print ("Enemy gaat er uit");
			}
			else {
				for (int e = 0; e < allyList.Count; e++) {
					if (col.gameObject == allyList [e]) {
						GameObject ally = col.gameObject;
						ally.GetComponent<TestPlayer> ().insideTurret = false;
						ally.GetComponent<TestPlayer> ().turretIAmIn = null;
						allyList.Remove (ally);
						break;
					}
				}
				print ("Ally gaat er uit");
			}
		}
	}

	public void Update(){
		StateChecker ();
	}
		
	public void StateChecker(){
		switch (myState) {
		case TowerState.Hitable:
		case TowerState.Protected:
			previousState = myState;
			TargetSelect ();
			break;	
		case TowerState.ProtectPlayer:
			TargetSelect ();
			break;
		}
	}
		
	public void ProtectPlayer(GameObject recievedTarget){
		if (enemyPlayer == null) {
			for (int i = 0; i < targetList.Count; i++) {
				if(targetList[i] == recievedTarget){
					enemyPlayer = recievedTarget;
					myState = TowerState.ProtectPlayer;
					break;
				}
			}
		}
	}

	public override void TargetSelect(){
		if(myState == TowerState.ProtectPlayer){
			if (enemyPlayer != null && !enemyPlayer.GetComponent<TestPlayer> ().dead) {
				Attack (enemyPlayer);
			}
			else {
				enemyPlayer = null;
				myState = previousState;
			}
		}
		else{
			if (targetList.Count > 0) {
				if (targetList [0] != null) {
					Attack (targetList [0]);
				}
				else {
					for(int i = 0; i < targetList.Count; i++){
						if (targetList [i] == null) {
							targetList.RemoveAt (i);
						} 
					}
				}
			}
		}
	}

	public override void Attack(GameObject myTarget){
		if(Time.time > nextAttack){
			ActivatePool(myTarget);
			nextAttack = Time.time + attackRate;
		}
	}

	public void ActivatePool(GameObject myTarget){
		for(int i = 0; i < pooledProjectiles.Length; i++){
			if(pooledProjectiles[i].GetComponent<ProjectileTower>().pooled){
				pooledProjectiles [i].GetComponent<ProjectileTower>().Unpool (myTarget);
				break;
			}
		}
	}

	public override void RecieveDamage(int recievedDamage){
		//Dit moet later uitgebreid worden met de damage stats enzo
		curHealth -= recievedDamage;
		HealthChecker ();
	}

	public override void HealthChecker(){
		if(curHealth < 1){
			curHealth = 0;
			Death ();
		}
	}

	public override void Death(){
		myState = TowerState.Dead;
		//Ik moet nog zien hoe kilian dit in elkaar gaat zettem
	}
}