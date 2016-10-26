using UnityEngine;
using System.Collections;

public class MinionStats : MonoBehaviour {

    private float timer;
    public SpawnManager spawnManager;

	// Use this for initialization
	void Start () {
        timer = 5;
	}
	
	// Update is called once per frame
	void Update () {
	    if(timer >= 0) {
            timer -= Time.deltaTime;
        }
        else {
            spawnManager.CheckCurrentWave();
            Destroy(gameObject);
        }
	}

    public void IncreaseStats(int incraseAmount) {
        print(incraseAmount);
    }
}
