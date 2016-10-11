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
		StartCoroutine(RespawnCamp(reSpawnTime));
	}

	IEnumerator RespawnCamp(float time) {
		yield return new WaitForSeconds(time);
		SpawnCamp();
	}

	public void CampMinionDied(){
		campCount--;
		if(campCount == 0){
			StartCoroutine(RespawnCamp(reSpawnTime));
		}
	}

	public void SpawnCamp(){
		for(int i = 0; i <= campArray.Length-1; i++){
			GameObject temp = Instantiate(campArray[i], spawnPointsArray[i].transform.position, transform.rotation) as GameObject;
			temp.GetComponent<JungleMinionBehavior>().minionCampNumber = campNumber;
		}

	}
}
