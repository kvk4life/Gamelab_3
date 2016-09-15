using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

	public int moveSpeed;
	public GameObject spawner;
	private MinionSpawner spawnScript;
	[SerializeField]
	private Transform curretTarget;
	private int counter;

	public List<Transform> wayPointList = new List<Transform>();


	void Start () {
		spawnScript = spawner.GetComponent<MinionSpawner>();
		AddWayPoints();
		curretTarget = wayPointList[counter];
	}

	void Update () {
		transform.LookAt(curretTarget.position);
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

	}

	void AddWayPoints(){
		for(int i = 0; i < spawnScript.waypointList.Length; i++){
			wayPointList.Add(spawnScript.waypointList[i]);
		}
	}

	void OnTriggerEnter(Collider wayPoint){
		if(wayPoint.transform == wayPointList[counter]){
			counter++;
			if(counter >= wayPointList.Count){
				counter = 0;
			}
			curretTarget = wayPointList[counter];
		}
	}
}
