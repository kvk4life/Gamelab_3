using UnityEngine;
using System.Collections;

public class JungleMinionStats : MonoBehaviour {

	public int health, minionCampNumber;
	private int maxHealth;
	public SpawnManager spawnScript;
	public GameObject spawnManagerObject;

	void Start () {
		maxHealth = health;
		spawnManagerObject = GameObject.FindGameObjectWithTag("SpawnManager");
		spawnScript = spawnManagerObject.GetComponent<SpawnManager>();
	}

	void Update () {
	}


	public void ResetHealth(){
		health = maxHealth;
	}
}
