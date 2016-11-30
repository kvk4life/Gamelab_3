using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {

    private float timer;
    public SpawnManager spawnManager;
    public float deathTime, health, damage;

	void Start () {
        timer = deathTime;
	}
	
	void Update () {
	    if(timer >= 0) {// remove later if you can kill enemies yourself
            timer -= Time.deltaTime;
        }
        else {
            spawnManager.CheckCurrentWave();
            Destroy(gameObject);
        }
	}

    public void IncreaseStats(int incraseAmount) {//increases stats of the enemies
        health = health / 100 * incraseAmount;
        damage = damage / 100 * incraseAmount;
    }

    public void Damage(float damage) {//deals damage to the enemies
        health -= damage;
        CheckDeath();
    }

    void CheckDeath() {//check if health is 0 to kill destroy the object and retrect a number from the enemy counter
        if(health <= 0) {
            spawnManager.CheckCurrentWave();
            Destroy(gameObject);
        }
    }
}
