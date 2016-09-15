using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : Base {
	public List<GameObject> targetList = new List<GameObject> ();
	public float attackRate;
	private float nextAttack;
	public GameObject[] pooledProjectiles;

	public void OnTriggerEnter(Collider col){
		if (col.GetComponent<Base> () != null) {
			if (col.GetComponent<Base> ().myTeam != myTeam) {
				//Het object wat door mij trigger veld heen kwam
				//is dus een unit van de enemy team
				targetList.Add(col.gameObject);
				print ("unit geadd aan target list");
			}
		}
	}

	public void OnTriggerExit(Collider col){
		print ("Iets gaat er uit");
		if (col.GetComponent<Base> () != null) {
			for(int e = 0; e < targetList.Count; e++){
				if (col.gameObject == targetList[e]) {
					targetList.Remove (col.gameObject);
					break;
				}
			}
		}
	}

	public void Update(){
		TargetSelect ();
	}

	public override void Attack(GameObject myTarget){
		if(Time.time > nextAttack){
			ActivatePool(myTarget);
			nextAttack = Time.time + attackRate;
		}
	}

	public void ActivatePool(GameObject myTarget){
		for(int i = 0; i < pooledProjectiles.Length; i++){
			if(pooledProjectiles[i].GetComponent<ProjectileTower>().pooled){
				pooledProjectiles [i].GetComponent<ProjectileTower>().Unpool (myTarget);
				break;
			}
		}
	}

	public override void TargetSelect(){
		if (targetList.Count > 0) {
			for(int i = 0; i < targetList.Count; i++){
				if (targetList [i] != null) {
					Attack (targetList [0]);
				} 
				else {
					targetList.RemoveAt (i);
				}
			}
		}
	}

	public override void HealthChecker(){
		
	}

	public override void Death(){
	
	}
}
