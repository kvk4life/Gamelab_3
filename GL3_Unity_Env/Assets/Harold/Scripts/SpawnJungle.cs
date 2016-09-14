using UnityEngine;
using System.Collections;

public class SpawnJungle : MonoBehaviour {

	public GameObject[] campArray, spawnPointsArray;
	private Vector3 spawnPossitionArray;
	public float reSpawnTime;
	public int campNumber;
	private int campCount;

	void Start () {
		campCount = campArray.Length;
	}

	IEnumerator RespawnCamp(float time) {
		yield return new WaitForSeconds(time);
		SpawnCamp();
	}

	public void CampMinionDied(){
		campCount--;
		if(campCount == 0){
			RespawnCamp(reSpawnTime);
		}
	}

	public void SpawnCamp(){
		for(int i = 0; i <= campArray.Length; i++){
			//GameObject temp = instance i at spawnpost i as gameobject
			// give campnumber to minion.
		}

	}
}
