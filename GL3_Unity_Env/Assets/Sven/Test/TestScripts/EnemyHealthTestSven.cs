using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthTestSven : MonoBehaviour {

	public int health;

	public Combat combat;
	public Orbit orbit;

	void Start () {
	
		combat = GameObject.Find("Pig Benis01").GetComponent<Combat>();
		orbit = GameObject.Find("Camera").GetComponent<Orbit>();

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

		combat.lockSwitch = false;
		orbit.camMode = CameraMode.ThirdPerson;
		combat.lockedOn.Remove(transform);
	}
}
