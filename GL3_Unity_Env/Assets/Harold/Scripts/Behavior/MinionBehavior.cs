using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

	public int moveSpeed;
	public string[] taggList;
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

	void OnTriggerEnter(Collider trigger){
		if(trigger.transform == wayPointList[counter]){
			counter++;
			if(counter >= wayPointList.Count){
				counter = 0;
			}
			curretTarget = wayPointList[counter];
		}
		else{
			Agro(trigger.gameObject);
		}

	}


	void Agro(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				curretTarget = target.transform;
				break;
			}
		}	
	}
}
