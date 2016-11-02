using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthTestSven : MonoBehaviour {

	public int health;

	int maxHealth;

	public Combat combat;
	public Orbit orbit;

	public int maxPoints;

	PointMng pointManager;

	void Start () {

		maxHealth = health;
	
		combat = GameObject.Find("Pig Benis01").GetComponent<Combat>();
		orbit = GameObject.Find("Camera").GetComponent<Orbit>();

		pointManager = GameObject.Find("Pig Benis01").GetComponent<PointMng>();

	}
	
	void Update () {
	
	}

	public void EnemyHealth (int damage){

		health -= damage;

		int sendPoints = maxPoints / maxHealth * damage;

		pointManager.AddPoints(sendPoints);

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
