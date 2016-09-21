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
	public int teamNumber; //tijdelijk om te checcken voor allie of enemy

	private List<Transform> wayPointList = new List<Transform>();
	private List<GameObject> enemyList = new List<GameObject>();
	private List<GameObject> allyList = new List<GameObject>();


	void Start () {
		spawnScript = spawner.GetComponent<MinionSpawner>();
		AddWayPoints();
		curretTarget = wayPointList[counter];
	}

	void Update () {
		if(enemyList.Count > 0){
			curretTarget = enemyList[0].transform;
		}
		else{
			curretTarget = wayPointList[counter];
		}

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

		if(trigger.gameObject.GetComponent<MinionBehavior>() != null){
			AgroAdd(trigger.gameObject);
		}

	}

	void OnTriggerExit(Collider trigger){
		if(trigger.GetComponent<MinionBehavior>() != null){
			AgroRemove(trigger.gameObject);
		}

	}

	void AgroRemove(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.gameObject.GetComponent<MinionBehavior>().teamNumber != teamNumber){
					enemyList.Remove(target);
				}
				else{
					allyList.Remove(target);
					break;
				}
			}
		}
	}

	void AgroAdd(GameObject target){
		for(int i = 0; i <= taggList.Length-1; i++){
			if(target.transform.tag == taggList[i]){
				if(target.GetComponent<MinionBehavior>().teamNumber != teamNumber){
					enemyList.Add(target);
					print("enemyAdded");
				}
				else{
					allyList.Add(target);
					break;
				}
				//curretTarget = target.transform;

			}
		}	
	}
}
