using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

	public GameObject[] minionList;
	public Transform[] waypointList;
	public int team;
	public float nextWaveTime;
	public int maxMeleeMinions, maxRangeMinions, superMinionCounter;
	public float timeBetweenMinions;
	private int currentWaveMinions, minionNumber, superMinionCounterReset, leftToSpawn, maxMinions;
	public bool enemyBool;

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
		GameObject temp = Instantiate(minionList[MinionToSpawn], transform.position, transform.rotation) as GameObject;
		temp.GetComponent<MinionBehavior>().spawner = gameObject;
		if(team == 1){
			temp.GetComponent<MinionBehavior>().enemy = false;
		}
		else{
			temp.GetComponent<MinionBehavior>().enemy = true;
		}
	}
}

