using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	public int damage;

	public bool mayAttack;
	public bool lockSwitch;

	public float radius;

	public LayerMask mask;

	public List<GameObject> lockedOn = new List<GameObject>();

	public DelegateWeapons delegateWeapon;
	public EnemyHealthTestSven healthEnemy;

	void Start () {

		mayAttack = true;
		delegateWeapon = GetComponent<DelegateWeapons>();
	
	}
	
	void Update () {

		FillDelegate ();

		if(Input.GetButtonDown("R3")){
			lockSwitch =! lockSwitch;
			LockOn(lockSwitch);
		}
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

	public void LockOn (bool mayLock){

		if(mayLock == true){
			Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask);
	        foreach (Collider hit in colliders) {
	        	if(!lockedOn.Contains(hit.transform.gameObject)){
	        		lockedOn.Add(hit.transform.gameObject);
	        	}
	        }
        }
	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}


}
