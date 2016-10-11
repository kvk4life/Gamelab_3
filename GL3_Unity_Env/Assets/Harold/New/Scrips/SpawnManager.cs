using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    public GameObject[] spawnPointArray, minionArray; // Place all the spawnpoint of the level here. Place wich minions need to be spawned
    public string[] spawnPointTagArray; // All the tags of the spawnpoint. is needed to see wich spawnpoints need to be activated.
    public int[] spawnAmount; // how many of each enemy needs to be spawned
    public int currentSpawnAmount, increaseProcentPerWave, increaseStatsPerWave; //with how much % do you want to incease the stats and the amonut of enemies that need to be spawned
    public float timeBetweenWaves, timeBetweenMinions; // time between each wave after a wave is finished. spawn time between the minions
    private int maxSpawnAmount, waveCount;


    private List<GameObject> activeSpawnList = new List<GameObject>();
    private List<GameObject> offSpawnList = new List<GameObject>();

    void Start () {
        waveCount = -1;
        AddOffList(); // adds the waypoints to a non active list
        CheckForActivation(0); // Checks wich enemies needs to be activated
	}
	
	void Update () {
	
	}

    public void CheckCurrentWave() {
        if(currentSpawnAmount == 0) {
            StartCoroutine(StartNewWave(timeBetweenWaves));
        }
    }

    IEnumerator StartNewWave(float time) {
        yield return new WaitForSeconds(time);
        CheckWaveStats();
    }

    public void CheckForActivation(int index) {
        for (int i = 0; i < offSpawnList.Count; i++) {
            if (offSpawnList[i].tag == spawnPointTagArray[index]) {
                activeSpawnList.Add(offSpawnList[i]);
                offSpawnList.Remove(offSpawnList[i]);
            }
        }
    }

    void AddOffList() {
        for(int i = 0; i < spawnPointArray.Length; i++) {
            offSpawnList.Add(spawnPointArray[i]);
        }
    }

    void CheckWaveStats() {
        for (int i = 0; i < spawnAmount.Length; i++) {
            spawnAmount[i] = spawnAmount[i] / 100 * (100 + (increaseProcentPerWave * waveCount));
        }

        for (int i = 0; i < minionArray.Length; i++) {
            minionArray[i].GetComponent<MinionStats>().IncreaseStats(100 + increaseStatsPerWave * waveCount);
        }

        NewWave();
    }

    void NewWave() {
        waveCount++;
        for (int i = 0; i > spawnAmount.Length; i++) {
            maxSpawnAmount += spawnAmount[i];
        }

        for(int i = 0; i > activeSpawnList.Count; i++) {
            activeSpawnList[i].GetComponent<Spawner>().StartCoroutine(activeSpawnList[i].GetComponent<Spawner>().Spawning(timeBetweenMinions));
        }
    }
}
