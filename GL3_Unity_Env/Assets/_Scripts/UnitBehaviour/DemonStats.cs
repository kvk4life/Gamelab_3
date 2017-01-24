using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonStats : MonoBehaviour
{
    public SpawnManager spawnManager;
    public float health;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void IncreaseStats(int incraseAmount)
    {//increases stats of the enemies
        health = health / 100 * incraseAmount;
    }

    public void Damage(float damage)
    {//deals damage to the enemies
        health -= damage;
        anim.SetTrigger("DamageTaken");
        if (health < 1)
        {
            Death();
        }
    }

    void Death()
    {//check if health is 0 to kill destroy the object and retrect a number from the enemy counter
        spawnManager.CheckCurrentWave();
        GetComponent<DemonBehaviour>().EndLife();
    }
}
