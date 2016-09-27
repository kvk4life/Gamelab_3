﻿using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	int damageTest;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){

		if(col.transform.tag == "Enemy"){
			col.gameObject.GetComponent<EnemyHealthTestSven>().EnemyHealth(damageTest);
			print(col.transform.name);
		}
	}


	public void GetDamage (int damage){

		damageTest = damage;

	}
}
