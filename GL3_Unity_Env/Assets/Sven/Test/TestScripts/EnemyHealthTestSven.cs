using UnityEngine;
using System.Collections;

public class EnemyHealthTestSven : MonoBehaviour {

	public int health;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void EnemyHealth (int damage){

		health -= damage;

		if(health < 1){
			Destroy(gameObject);
		}
	}
}
