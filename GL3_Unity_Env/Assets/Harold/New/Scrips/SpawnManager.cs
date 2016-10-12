﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    public int increaseProcentPerWave, increaseStatsPerWave, startingWave; //with how much % do you want to incease the stats and the amonut of enemies that need to be spawned, the armount enemies currently spawned, the wave you start at
    public float timeBetweenWaves, timeBetweenMinions; // time between each wave after a wave is finished. spawn time between the minions
    private int maxSpawnAmount;
    [HideInInspector]
    public int waveCount, currentSpawnAmount;

    public List<GameObject> spawnPointArray = new List<GameObject>(); // Place all the spawnpoint of the level here. Place wich minions need to be spawned
    public List<string> spawnPointTagArray = new List<string>(); // All the tags of the spawnpoint. is needed to see wich spawnpoints need to be activated.
    public List<int> spawnAmount = new List<int>(); // how many of each enemy needs to be spawned
    public List<GameObject> minionArray = new List<GameObject>();
    private List<GameObject> activeSpawnList = new List<GameObject>();
    private List<GameObject> offSpawnList = new List<GameObject>();
    private List<int> saveSpawnAmount = new List<int>();
    private List<int> amountForEachSpawner = new List<int>();

    void Start() {
        waveCount = startingWave;
        SetAmountLength();
        SaveSpawnAmount();
        AddOffList(); // adds the waypoints to a non active list
        CheckForActivation(0); // Checks wich enemies needs to be activated
    }

    public void CheckCurrentWave() {
        if (currentSpawnAmount == 0) {
            StartCoroutine(StartNewWave(timeBetweenWaves));
        }
    }

    IEnumerator StartNewWave(float time) {
        yield return new WaitForSeconds(time);
        CheckWaveStats();
    }

    public void CheckForActivation(int index) {
        for (int i = 0; i < offSpawnList.Count;) {
            if (offSpawnList[i].tag == spawnPointTagArray[index]) {
                offSpawnList[i].GetComponent<Spawner>().spawnManager = GetComponent<SpawnManager>();
                activeSpawnList.Add(offSpawnList[i]);
                offSpawnList.Remove(offSpawnList[i]);
            }
        }
        CheckCurrentWave();
    }

    void AddOffList() {
        for (int i = 0; i < spawnPointArray.Count; i++) {
            offSpawnList.Add(spawnPointArray[i]);
        }
    }

    void CheckWaveStats() {
        waveCount++;
        ResetSpawnAmount();
        for (int i = 0; i <= spawnAmount.Count - 1; i++) {
            float Calcu = increaseProcentPerWave * waveCount;
            Calcu = Calcu / 100;
            Calcu += 1;
            float temp = (float)spawnAmount[i];
            temp = temp * Calcu;
            spawnAmount[i] = (int)temp;
        }

        for (int i = 0; i < minionArray.Count; i++) {
            minionArray[i].GetComponent<MinionStats>().IncreaseStats(100 + (increaseStatsPerWave * waveCount));
        }

        SaveSpawnAmount();
        NewWave();
    }

    void SetAmountLength() {
        for (int i = 0; i <= spawnAmount.Count - 1; i++) {
            saveSpawnAmount.Add(0);
            amountForEachSpawner.Add(0);
        }
    }

    void ResetSpawnAmount() {
        for (int i = 0; i <= spawnAmount.Count - 1; i++) {
            spawnAmount[i] = saveSpawnAmount[i];
        }
    }

    void SaveSpawnAmount() {
        for (int i = 0; i < spawnAmount.Count; i++) {
            saveSpawnAmount[i] = spawnAmount[i];
        }
    }

    void NewWave() {
        for (int i = 0; i < spawnAmount.Count; i++) {
            maxSpawnAmount += spawnAmount[i];
        }

        for (int ii = 0; ii < spawnAmount.Count; ii++) {
            //float count = activeSpawnList.Count;
            //print(count);
           // float temp = (float)spawnAmount[ii];
          //  print(temp);
           // temp = temp / count;
            float temp = spawnAmount[ii] / activeSpawnList.Count;
            amountForEachSpawner[ii] = (int)temp;
            print(temp);
            if (temp < 1) {
                temp = 1;
            }
            amountForEachSpawner[ii] = (int)temp;
        }

        for (int i = 0; i < activeSpawnList.Count; i++) {
            Spawner tempClass = activeSpawnList[i].GetComponent<Spawner>();

            for (int ii = 0; ii <= spawnAmount.Count-1; ii++) {
                tempClass.spawnAmount.Add(amountForEachSpawner[ii]);
                spawnAmount[ii] -= amountForEachSpawner[ii];
            }
        }

        for (int i = 0; i < spawnAmount.Count; i++) {
            maxSpawnAmount -= spawnAmount[i];
        }

        currentSpawnAmount = maxSpawnAmount;
        print(currentSpawnAmount);

        for (int i = 0; i < activeSpawnList.Count; i++) {
            // activeSpawnList[i].GetComponent<Spawner>().StartCoroutine(activeSpawnList[i].GetComponent<Spawner>().Spawning(timeBetweenMinions));
        }
    }
}