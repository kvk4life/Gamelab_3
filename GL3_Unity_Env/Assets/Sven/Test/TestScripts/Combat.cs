using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int attackDamage;

	public float rayDis;

	public RaycastHit rayHit;

	public int [] attackDamageStats;
	public float [] cooldownStats;
 
	delegate void AttackDelegate(int num);
	AttackDelegate myAttack;

	void Start () {
	
	}
	
	void Update () {

		if(Input.GetButtonDown("Attack Left")){
			myAttack = AttackLeft;
			myAttack(1);
		}

		if(Input.GetButtonDown("Attack Right")){
			myAttack = AttackLeft;
			myAttack(2);
		}
	
	}

	public void AttackLeft (int num) {

		print("Weapon:" + num);

	}

	public void AttackRight (int num) {

		print("Weapon:" + num);

	}
}
