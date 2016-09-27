using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int currentHealth, startHealth, damage, teamNumber;
    private int maxHealth;


	// Use this for initialization
	void Start () {
        maxHealth = startHealth;
        currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HealthReset(){
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage) {
        //damage calculation
        currentHealth -= damage;
    }
}
