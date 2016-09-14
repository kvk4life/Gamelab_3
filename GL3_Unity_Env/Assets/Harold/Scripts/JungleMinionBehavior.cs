using UnityEngine;
using System.Collections;

public class JungleMinionBehavior : MonoBehaviour {

	public JungleMinionStats jungleMinionStats;


	void Start () {
		jungleMinionStats = GetComponent<JungleMinionStats>();
	}

	void Update () {

	}

	public void Damaged(int Damage, bool magic){
		//armour or magic ressist retract
		jungleMinionStats.health-= Damage;

		if(jungleMinionStats.health <= 0){
			jungleMinionStats.spawnScript.jungleArray[jungleMinionStats.minionCampNumber].GetComponent<SpawnJungle>().CampMinionDied();
			Destroy(gameObject);
		}
	}
}
