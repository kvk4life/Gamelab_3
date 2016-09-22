using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	public int damage;

	public bool mayAttack;
	public bool lockSwitch;

	public float radius;

	public LayerMask mask;

	public List<Transform> lockedOn = new List<Transform>();

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
	        	if(!lockedOn.Contains(hit.transform)){
	        		lockedOn.Add(hit.transform);
	        	}
	        }
	        GetClosestEnemy(lockedOn);
        }
	}

	Transform GetClosestEnemy (List<Transform> closeEnemyList){

		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach(Transform t in closeEnemyList){
			float dist = Vector3.Distance(t.position, currentPos);
			if(dist < minDist){
				tMin = t;
				minDist = dist;
			}
		}

		print(tMin);
		return tMin;

	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}
}
