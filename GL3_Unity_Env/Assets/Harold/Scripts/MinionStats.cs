using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {

	public int health, minionCampNumber;
	private int maxHealth;
	private SpawnManager spawnScript;
	public GameObject spawnManagerObject;

	void Start () {
		maxHealth = health;
		spawnScript = spawnManagerObject.GetComponent<SpawnManager>();
	}

	void Update () {
	}

	public void Damaged(int Damage){
		health-= Damage;

		if(health <= 0){
			spawnScript.jungleArray[minionCampNumber].GetComponent<SpawnJungle>().CampMinionDied();
			Destroy(gameObject);
		}
	}

}
