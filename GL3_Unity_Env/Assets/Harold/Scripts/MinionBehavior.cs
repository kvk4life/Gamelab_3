using UnityEngine;
using System.Collections;

public class MinionBehavior : MonoBehaviour {
	
	public MinionStats minionStats;


	void Start () {
		minionStats = GetComponent<MinionStats>();
	}

	void Update () {
	
	}

	public void Damaged(int Damage, bool magic){
		//armour or magic ressist retract
		minionStats.health-= Damage;

		if(minionStats.health <= 0){
			minionStats.spawnScript.jungleArray[minionStats.minionCampNumber].GetComponent<SpawnJungle>().CampMinionDied();
			Destroy(gameObject);
		}
	}
}
