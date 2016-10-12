using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public GameObject[] minionObjects;
    public SpawnManager spawnManager;
    public int test;

    public List<int> spawnAmount = new List<int>();


    // Use this for initialization
    void Start () {
        //test = 20;
      //  WhileTest();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void WhileTest() {
      //  while(test > 0) {

       // }
    }

    public IEnumerator Spawning(float timer) {
        yield return new WaitForSeconds(timer);
    }
}
