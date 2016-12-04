using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {
    public SpawnManager spawnManager;
    public float health, heavyDamageChance, maxHeavyDamageChance;
    public PBAragdollController rdControl;

	void Start () {
        rdControl = GetComponent<PBAragdollController>();
	}
	
	void Update () {
        
	}

    public void IncreaseStats(int incraseAmount) {//increases stats of the enemies
        health = health / 100 * incraseAmount;
    }

    public void Damage(float damage) {//deals damage to the enemies
        health -= damage;
        CheckHeavyDamage();
        if (health < 1) {
            Death();
        }
    }

    void CheckHeavyDamage() {
        float heavyDamageRandom = Random.Range(0, maxHeavyDamageChance);
        if (heavyDamageRandom < heavyDamageChance)
        {
            rdControl.HeavyDamage();
        }
    }

    void Death() {//check if health is 0 to kill destroy the object and retrect a number from the enemy counter
        spawnManager.CheckCurrentWave();
        GetComponent<MinionBehavior>().EndLife();
        rdControl.KillRagdoll();
    }
}
