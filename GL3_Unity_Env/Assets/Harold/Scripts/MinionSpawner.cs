using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

	public GameObject[] minionList;
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

	void MinionSpawn(int MinionToSpawn){
		Instantiate(minionList[MinionToSpawn], transform.position, transform.rotation);
	}
}

