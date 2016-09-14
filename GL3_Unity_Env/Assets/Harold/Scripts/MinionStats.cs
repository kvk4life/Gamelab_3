using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {

	public int health, minionCampNumber;
	private int maxHealth;
	public SpawnManager spawnScript;
	public GameObject spawnManagerObject;

	void Start () {
		maxHealth = health;
		spawnScript = spawnManagerObject.GetComponent<SpawnManager>();
	}

	void Update () {
	}


	public void ResetHealth(){
		health = maxHealth;
	}
}
