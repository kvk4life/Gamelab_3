﻿using UnityEngine;
using System.Collections;

public class DelegateWeapons : MonoBehaviour {

	public delegate void RightHandedLight (int damage, float cooldown);
	public delegate void LeftHandedLight (int damage, float cooldown);
	public delegate void RightHandedHeavy (int damage, float cooldown);
	public delegate void LeftHandedHeavy (int damage, float cooldown);
	public delegate void ChargeAttack (int damage);

	public RightHandedLight rhLight;
	public LeftHandedLight lhLight;
	public RightHandedHeavy rhHeavy;
	public LeftHandedHeavy lhHeavy;
	public ChargeAttack charge;

	public int damageRHL, damageRHH, damageLHL, damageLHH;

	public float cooldownRHL, cooldownRHH, cooldownLHL, cooldownLHH;

	void Start () {
	
	}
	
	void Update () {
		
		TriggerDetect();

	}

	public void TriggerDetect (){

		float getInputTriggers;
		bool getInputCharge;

		getInputTriggers = Input.GetAxis("Triggers");
		getInputCharge = Input.GetAxis("Triggers") < 0;

		if(getInputTriggers < 0){
			if(rhHeavy != null){
				rhHeavy (damageRHH, cooldownRHH);
			}
		}
		else if(getInputTriggers > 0){
			if(lhHeavy != null){
				lhHeavy (damageLHH, cooldownLHH);
			}
		}

		/*if(Input.GetButtonDown("LB")){
			if(lhLight != null){
				lhLight (damageLHL, cooldownLHL);
			}
		}*/

		if(Input.GetButtonDown("RB")){
			if(rhLight != null){
				rhLight (damageRHL, cooldownRHL);
			}
		}

		if(getInputCharge == true){
			print("LOL");
		}
	}
}


