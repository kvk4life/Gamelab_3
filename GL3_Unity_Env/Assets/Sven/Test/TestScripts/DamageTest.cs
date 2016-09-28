using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	int damageTest;

	GameObject player;

	void Start () {

		player = GameObject.Find("Pig Benis01");
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){

		if(col.transform.tag == "Enemy" && player.GetComponent<Combat>().mayAttack == false){
			col.gameObject.GetComponent<EnemyHealthTestSven>().EnemyHealth(damageTest);
			print(col.transform.name);
		}
	}


	public void GetDamage (int damage){

		damageTest = damage;

	}
}
