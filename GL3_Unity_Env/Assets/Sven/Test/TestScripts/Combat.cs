using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int damage;

	public float cooldown;

	public bool mayAttack;

	public DelegateWeapons delegateWeapon;
	public EnemyHealthTestSven healthEnemy;

	void Start () {

		mayAttack = true;
		delegateWeapon = GetComponent<DelegateWeapons>();
	
	}
	
	void Update () {

		FillDelegate ();
	
	}

	public void FillDelegate (){

		delegateWeapon.rhLight = null;
		delegateWeapon.lhLight = null;
		delegateWeapon.rhLight = Attack;
		delegateWeapon.lhLight = Attack;

	}


	public void Attack () {
		
		if(mayAttack == true){
			StartCoroutine(CoolDown(cooldown));
			print ("Doet Het");
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
