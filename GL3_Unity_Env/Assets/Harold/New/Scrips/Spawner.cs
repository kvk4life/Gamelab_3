using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public GameObject[] minionObjects;
    public SpawnManager spawnManager;

    public List<int> spawnAmount = new List<int>();

    public IEnumerator Spawning(float timer) { //spawns the enemies
        yield return new WaitForSeconds(timer);
        if(spawnAmount[0] > 0) {
            spawnManager.CheckCurrentWave();
            spawnAmount[0]--;
            print("spawned");
            StopCoroutine("Spawning");
            StartCoroutine(Spawning(timer));
        }
        else {
            if (0 != spawnAmount.Count-1) {
                spawnAmount.Remove(spawnAmount[0]);
                StopCoroutine("Spawning");
                StartCoroutine(Spawning(timer));
            }
            else {
                print("done");
                StopCoroutine("Spawning");
            }
        }
    }
}
