﻿using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

	public GameObject[] minionList;
	public Transform[] waypointList;
	public int team;
	public float nextWaveTime, timeBeforeFirstWave;
	private bool gameStart;
	public int[] maxMinionsList;
	public int superMinionCounter;
	public float timeBetweenMinions;
	private int currentWaveMinions, minionNumber, superMinionCounterReset, leftToSpawn, maxMinions;

	void Start () {
		gameStart = true;
		StartMinionSpawn();
		superMinionCounterReset = superMinionCounter;
	}
		
	public void StartMinionSpawn(){
		StartCoroutine(SpawnWave(nextWaveTime));
	}

	IEnumerator SpawnWave(float time) {
		if(gameStart){
			yield return new WaitForSeconds(time);
			gameStart = false;
			time = nextWaveTime;
		}
		currentWaveMinions = 0;
		minionNumber = 0;
		superMinionCounter--;
		AddToLeftToSpawn(minionNumber);
		StartCoroutine(SpawnMinions(timeBetweenMinions));
		yield return new WaitForSeconds(time);
		StartCoroutine(SpawnWave(nextWaveTime));
	}

	IEnumerator SpawnMinions(float time){
		yield return new WaitForSeconds(time);
		CountTotalMinions();

		if(currentWaveMinions == maxMinions && superMinionCounter == 0){
			minionNumber++;
			MinionSpawn(minionNumber);
			superMinionCounter = superMinionCounterReset;
		}
			
		if(currentWaveMinions == maxMinions){
			StopCoroutine("SpawnMinions");
		}
		else{
			
			if(leftToSpawn == 0){
				minionNumber++;
				AddToLeftToSpawn(minionNumber);
			}

			MinionSpawn(minionNumber);
			leftToSpawn--;
			currentWaveMinions++;
			StartCoroutine(SpawnMinions(timeBetweenMinions));
		}
	}

	void CountTotalMinions(){
		for(int i = 0; i <= maxMinionsList.Length; i++){
			maxMinions+= maxMinionsList[i];
		}
	}

	void AddToLeftToSpawn(int NumberMinion){
		switch (NumberMinion){
		case 0:
			leftToSpawn+= maxMinionsList[NumberMinion];
			break;
		case 1:
			leftToSpawn+= maxMinionsList[minionNumber];
			break;
		}
	}

	void MinionSpawn(int MinionToSpawn){
		GameObject temp = Instantiate(minionList[MinionToSpawn], transform.position, transform.rotation) as GameObject;
		temp.GetComponent<MinionBehavior>().spawner = gameObject;
		temp.GetComponent<MinionBehavior>().teamNumber = team;
	}
}

