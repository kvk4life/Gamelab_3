using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

	public float nextWaveTime;
	public int maxMeleeMinions, maxRangeMinions, superMinionCounter;
	public float timeBetweenMinions;
	private int currentWaveMinions, minionNumber, superMinionCounterReset, leftToSpawn, maxMinions;

	void Start () {
		StartMinionSpawn();
		superMinionCounterReset = superMinionCounter;
	}
		
	public void StartMinionSpawn(){
		StartCoroutine(SpawnWave(nextWaveTime));
	}

	IEnumerator SpawnWave(float time) {
		yield return new WaitForSeconds(time);
		print("Spawn Wave");
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
		maxMinions = maxMeleeMinions + maxRangeMinions;

		if(currentWaveMinions == maxMinions && superMinionCounter == 0){
			minionNumber++;
			MinionList(minionNumber);
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

			MinionList(minionNumber);
			leftToSpawn--;
			currentWaveMinions++;
			StartCoroutine(SpawnMinions(timeBetweenMinions));
		}
	}

	void AddToLeftToSpawn(int NumberMinion){
		switch (NumberMinion){
		case 0:
			leftToSpawn+= maxMeleeMinions;
			break;
		case 1:
			leftToSpawn+= maxRangeMinions;
			break;
		}
	}

	void MinionList(int MinionToSpawn){
		
		switch (MinionToSpawn){
		case 0:
			print("Melee spawned");
			break;
		case 1:
			print("Range spawned");
			break;
		case 2:
			print("Super Spawned");
			break;

		}
	}
}

