using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthTestSven : MonoBehaviour {

	public int health;

	public GameObject player;

	Combat combat;

	void Start () {
	
		player = GameObject.Find("Pig Benis01");
		combat = GameObject.Find("Pig Benis01").GetComponent<Combat>();

	}
	
	void Update () {
	
	}

	public void EnemyHealth (int damage){

		health -= damage;

		if(health < 1){
			RemoveIndex();
			Destroy(gameObject);
		}
	}

	public void RemoveIndex (){

		combat.lockedOn.Remove(transform);
	}
}
