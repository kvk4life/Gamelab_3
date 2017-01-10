using UnityEngine;
using System.Collections;

public class Crosbow : Weapon_Manager {

	void Start () {

		base.mayFire = true;
	
	}
	

	void Update () {
	
	}

	protected override void FireWeapon (){



	}

	protected override void FillDelegate (){


	}
		
	protected override void WeaponStats (int damage, float rPM, float range, float speed){



	}

	protected override IEnumerator Cooldown (float cooldown){

		base.mayFire = false;
		yield return new WaitForSeconds(cooldown);
		base.mayFire = true;

	}
}
