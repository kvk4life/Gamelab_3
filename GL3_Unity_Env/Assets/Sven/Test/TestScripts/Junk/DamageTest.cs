﻿using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	public float damageTest;

	GameObject player;

	void Start () {

		player = GameObject.Find("AdolfFunctionaliteiten");
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){

		if(col.transform.tag == "Enemy" || col.transform.tag == "Minion" && player.GetComponent<Combat>().mayAttack == false){
			col.gameObject.GetComponent<MinionStats>().Damage(damageTest);
		}
	}


	public void GetDamage (int damage){

		damageTest = damage;

	}
}
