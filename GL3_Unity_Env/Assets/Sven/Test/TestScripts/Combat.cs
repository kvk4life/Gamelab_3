using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int damage;

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
		delegateWeapon.rhHeavy = null;
		delegateWeapon.lhHeavy = null;
		delegateWeapon.rhLight = Attack;
		delegateWeapon.lhLight = Attack;
		delegateWeapon.rhHeavy = Attack;
		delegateWeapon.lhHeavy = Attack;

	}


	public void Attack (int damage, float cooldown) {
		
		if(mayAttack == true){
			StartCoroutine(CoolDown(cooldown));
			print (damage);
		}
	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}


}
