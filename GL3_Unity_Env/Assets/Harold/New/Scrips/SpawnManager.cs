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

    [SerializeField]
    private List<GameObject> activeSpawnList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> offSpawnList = new List<GameObject>();
    [SerializeField]
    private List<int> saveSpawnAmount = new List<int>();

    void Start() {
        waveCount = -1;
        SetAmountLength();
        SaveSpawnAmount();
        AddOffList(); // adds the waypoints to a non active list
        CheckForActivation(0); // Checks wich enemies needs to be activated
    }

    void Update() {

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
        for (int i = 0; i < spawnPointArray.Length; i++) {
            offSpawnList.Add(spawnPointArray[i]);
        }
    }

    void CheckWaveStats() {
        waveCount++;
        ResetSpawnAmount();
        for (int i = 0; i < spawnAmount.Length; i++) {
            spawnAmount[i] = spawnAmount[i] / 100 * (100 + (increaseProcentPerWave * waveCount));
        }

        for (int i = 0; i < minionArray.Length; i++) {
            minionArray[i].GetComponent<MinionStats>().IncreaseStats(100 + increaseStatsPerWave * waveCount);
        }

        SaveSpawnAmount();
        NewWave();
    }

    void SetAmountLength() {
        for(int i = 0; i <= spawnAmount.Length-1; i++) {
            saveSpawnAmount.Add(0);
        }
    }

    void ResetSpawnAmount() {
        for(int i = 0; i <= spawnAmount.Length; i++) {
            spawnAmount[i] = saveSpawnAmount[i];
        }
    }

    void SaveSpawnAmount() {
        for (int i = 0; i < spawnAmount.Length; i++) {
            saveSpawnAmount[i] = spawnAmount[i];
        }
    }

    void NewWave() {
        for (int i = 0; i < spawnAmount.Length; i++) {
            maxSpawnAmount += spawnAmount[i];
        }

        for (int i = 0; i < activeSpawnList.Count; i++) {
            Spawner tempClass = activeSpawnList[i].GetComponent<Spawner>();

            for (int ii = 0; ii < spawnAmount.Length; ii++) {
                if (spawnAmount[ii] > 0) {
                    int tempInt = spawnAmount[ii] / activeSpawnList.Count;
                    print(tempInt);
                    if(tempInt <= 0) {
                        tempInt = 1;
                    }
                    tempClass.spawnAmount.Add(tempInt);
                    spawnAmount[ii] -= tempInt;
                }

                
            }
        }

        for(int i = 0; i < activeSpawnList.Count; i++) {
           // activeSpawnList[i].GetComponent<Spawner>().StartCoroutine(activeSpawnList[i].GetComponent<Spawner>().Spawning(timeBetweenMinions));
        }
    }
}
