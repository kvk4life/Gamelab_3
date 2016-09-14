using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int attackDamageLeft, attackDamageRight;

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

		float getInput;
		getInput = Input.GetAxis("Triggers");

		if(getInput < 0 && mayAttack == true){
			RaycastShooter(transform.position, transform.forward,  rayHit, rayDis, getInput);
		}
		else if(getInput > 0 && mayAttack == true){
			RaycastShooter(transform.position, transform.forward,  rayHit, rayDis, getInput);
		}

	}

	public void AttackLeft (int attackDamage) {

		print("DamageLeft:" + attackDamage);
		
		StartCoroutine(CoolDown(cooldownStats));
		healthEnemy = rayHit.transform.gameObject.GetComponent<EnemyHealthTestSven>();
		healthEnemy.EnemyHealth(attackDamage);
	}

	public void AttackRight (int attackDamage) {

		print("DamageRight:" + attackDamage);

	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}

	public void RaycastShooter (Vector3 origin, Vector3 direction, RaycastHit hit, float distance, float input){

		if(Physics.Raycast(origin, direction, out hit, distance) && input < 0){
			if(hit.transform.tag == "Enemy"){
				rayHit = hit;
				myAttack = AttackRight;
				myAttack(attackDamageRight);
				print("IsHittedRight");
			}
		}

		if(Physics.Raycast(origin, direction, out hit, distance) && input > 0){
			if(hit.transform.tag == "Enemy"){
				rayHit = hit;
				myAttack = AttackLeft;
				myAttack(attackDamageLeft);
				print("IsHittedLeft");
			}
		}
	}

	public void TriggerDetectTest (){					//Weghalen nadat het systeem volledig werkt met controller

		if(Input.GetButtonDown("Attack Left")){
			myAttack = AttackLeft;
			myAttack(10);
		}

		if(Input.GetButtonDown("Attack Right")){
			myAttack = AttackLeft;
			myAttack(25);
		}
	}

}
