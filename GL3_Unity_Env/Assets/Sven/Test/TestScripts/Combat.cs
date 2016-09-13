using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int attackDamage;

	public float rayDis;
	public float cooldownStats;

	public RaycastHit rayHit;

	public bool mayAttack;

	public EnemyHealthTestSven healthEnemy;
	
	delegate void AttackDelegate(int num);
	AttackDelegate myAttack;

	void Start () {

		mayAttack = true;
	
	}
	
	void Update () {

		TriggerDetect ();
		TriggerDetectTest();							//Weghalen nadat het systeem volledig werkt met controller
	
	}

	public void TriggerDetect () {

		if(Input.GetAxis("Triggers") < 0 && mayAttack == true){
			myAttack = AttackRight;
			myAttack(2);
		}
		else if(Input.GetAxis("Triggers") > 0 && mayAttack == true){
			myAttack = AttackLeft;
			myAttack(1);
		}
	}

	public void AttackLeft (int num) {

		print("Weapon:" + num);

		StartCoroutine(CoolDown(cooldownStats));
		if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayDis)){
			if(rayHit.transform.tag == "Enemy"){
				healthEnemy = rayHit.transform.gameObject.GetComponent<EnemyHealthTestSven>();
				healthEnemy.EnemyHealth(attackDamage);
			}
		}
	}

	public void AttackRight (int num) {

		print("Weapon:" + num);

	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}

	public void TriggerDetectTest (){					//Weghalen nadat het systeem volledig werkt met controller

		if(Input.GetButtonDown("Attack Left")){
			myAttack = AttackLeft;
			myAttack(1);
		}

		if(Input.GetButtonDown("Attack Right")){
			myAttack = AttackLeft;
			myAttack(2);
		}
	}

}
