using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {

	public int health;
	private int maxHealth;

	void Start () {
		maxHealth = health;
	}

	public void ResetHealth(){
		health = maxHealth;
	}
}
