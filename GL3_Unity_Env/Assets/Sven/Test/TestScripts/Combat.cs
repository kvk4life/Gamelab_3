using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public float cooldownStatsLeft, cooldownStatsRight;

	public bool mayAttack;

	public EnemyHealthTestSven healthEnemy;
	
	delegate void AttackDelegate(int num);
	AttackDelegate myAttack;

	void Start () {

		mayAttack = true;
	
	}
	
	void Update () {

	
	}

	public void FillDelegate (){


	}


	public void Attack (int attackDamage) {
		
		if(mayAttack == true){
			StartCoroutine(CoolDown(cooldownStatsLeft));
			//healthEnemy = rayHit.transform.gameObject.GetComponent<EnemyHealthTestSven>();
			//healthEnemy.EnemyHealth(attackDamage);
		}
	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}


}
