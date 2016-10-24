using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	public int damage;

	public bool mayAttack;
	public bool lockSwitch;

	public float radius;
	public float damping;

	public GameObject attackCollision;
	public GameObject camera;
	public GameObject lookAtBody;
	public Transform closestEnemy;

	public Animator anim;

	public LayerMask mask;

	public List<Transform> lockedOn = new List<Transform>();

	public DelegateWeapons delegateWeapon;
	public EnemyHealthTestSven healthEnemy;
	public Orbit orbit;

	void Start () {

		mayAttack = true;
		delegateWeapon = GetComponent<DelegateWeapons>();
	
	}
	
	void Update () {

		FillDelegate ();

		if(Input.GetButtonDown("R3")){
			lockSwitch = true;
			LockOn(lockSwitch);
			GetClosestEnemy(lockedOn);
		}
		
	}

	void LateUpdate (){

		if(closestEnemy != null){
			LookAtLockOn();
		}

	}

	public void FillDelegate (){

		delegateWeapon.rhLight = null;
		delegateWeapon.lhLight = null;
		delegateWeapon.rhHeavy = null;
		delegateWeapon.lhHeavy = null;
		delegateWeapon.charge = null;
		delegateWeapon.rhLight = Attack;
		delegateWeapon.lhLight = Attack;
		delegateWeapon.rhHeavy = Attack;
		delegateWeapon.lhHeavy = Attack;
		delegateWeapon.charge = Attack;

	}


	public void Attack (int damage, float cooldown) {
		
		if(mayAttack == true){
			StartCoroutine(CoolDown(cooldown));
			anim.SetTrigger("Attack01");
			attackCollision.GetComponent<DamageTest>().GetDamage(damage);
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
        }
	}

	public Transform GetClosestEnemy (List<Transform> closeEnemyList){

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
		camera.transform.LookAt(new Vector3 (tMin.position.x, transform.position.y / 2, tMin.position.z));
		closestEnemy = tMin;
		return tMin;

	}

	public void LookAtLockOn () {

		var lookPos = closestEnemy.position - transform.position;
		lookPos.y = 0;

		var rotation = Quaternion.LookRotation(lookPos);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

	}

	IEnumerator CoolDown (float cooldown){

		mayAttack = false;
		yield return new WaitForSeconds(cooldown);
		print("Recharged");
		mayAttack = true;
	}

    void OnCollisionEnter()
    {
    	orbit.mayJump=true;
    }
}
