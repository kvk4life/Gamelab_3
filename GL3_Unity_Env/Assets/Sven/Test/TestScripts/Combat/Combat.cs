using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Sven Evertse ©

public class Combat : MonoBehaviour {

	public int damage;
	public int damagePar;

	public bool mayAttack;
	public bool lockSwitch;

	public float radius;
	public float damping;
	public float rangeShit;

	public GameObject attackCollision;
	public GameObject camera;
	public GameObject lookAtBody;
	public GameObject shitParticle;
	public Transform closestEnemy;
	public Transform particleTrans;

	public Animator anim;

	public LayerMask mask;

	public RaycastHit hit;

	public List<Transform> lockedOn = new List<Transform>();

	public DelegateWeapons delegateWeapon;
	public EnemyHealthTestSven healthEnemy;
	public Orbit orbit;

	void Start () {

		attackCollision = GetComponent<WeaponPickup>().weapons[0];
		mayAttack = true;
		delegateWeapon = GetComponent<DelegateWeapons>();
		shitParticle.SetActive(false);
	
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


	public void Attack (int damage, float cooldown, int checkAnim) {
		
		if(mayAttack == true){
			StartCoroutine(CoolDown(cooldown));
			attackCollision.GetComponent<DamageTest>().GetDamage(damage);
			print(checkAnim);

			if(checkAnim == 2){
				anim.SetTrigger("Att L 0");
			}


			if(checkAnim == 1){
				anim.SetTrigger("Att H 0");
			}

			if(checkAnim == 3){
				anim.SetTrigger("Ch Att 0");
				damagePar = damage;
				StartCoroutine(ParticleEnabler());
			}
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

	IEnumerator ParticleEnabler (){

		shitParticle.SetActive(true);
		yield return new WaitForSeconds(0.8f);
		print("Particle");
		shitParticle.SetActive(false);
		if(Physics.Raycast(particleTrans.position, particleTrans.transform.up, out hit, rangeShit)){
			print(hit);
			Debug.DrawRay(particleTrans.position, particleTrans.transform.up, Color.red);
			if(hit.transform.tag == "Enemy"){
				hit.transform.gameObject.GetComponent<EnemyHealthTestSven>().EnemyHealth(damagePar);
			}
		}
	}

    void OnCollisionEnter()
    {
    	orbit.mayJump=true;
    }
}
