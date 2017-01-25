using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public int health;
	public Animator anim;

	void Start () {

		health = maxHealth;
	
	}

	public void GetDamage (int damage) {

		health -= damage;
		anim.SetTrigger("Get Hit");

        GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SoundManager>().HeartBeat(health);
        GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SoundManager>().PlayerHit();

        if (health <= 0){

			Destroy(gameObject);

		}
	}
}
