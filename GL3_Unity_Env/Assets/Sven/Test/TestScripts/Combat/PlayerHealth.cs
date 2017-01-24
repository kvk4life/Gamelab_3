using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public int health;
	public Animator anim;

	void Start () {

		health = maxHealth;
	
	}

	void Update () {


	
	}

	public void GetDamage (int damage) {

		health -= damage;
		anim.SetTrigger("Get Hit");

		if(health <= 0){

			Destroy(gameObject);

		}
	}
}
