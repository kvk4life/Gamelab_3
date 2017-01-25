using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	public int damageTest;

	public GameObject player;

	void Start () {

		player = GameObject.Find("Adolf");
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
        print("Ran");
		if(col.transform.tag == "Enemy" || col.transform.tag == "Minion"){
            print("ifwentwell");
			col.gameObject.GetComponent<DemonMakeShiftBs>().TakeDamage(damageTest);
		}
	}


	public void GetDamage (int damage){

		damageTest = damage;

	}
}
