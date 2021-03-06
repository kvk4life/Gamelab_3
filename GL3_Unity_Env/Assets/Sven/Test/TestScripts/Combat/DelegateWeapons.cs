﻿using UnityEngine;
using System.Collections;

//Sven Evertse ©

public class DelegateWeapons : MonoBehaviour {

	public delegate void RightHandedLight (int damage, float cooldown, int checkAnim);
	public delegate void LeftHandedLight (int damage, float cooldown, int checkAnim);
	public delegate void RightHandedHeavy (int damage, float cooldown, int checkAnim);
	public delegate void LeftHandedHeavy (int damage, float cooldown, int checkAnim);
	public delegate void ChargeAttack (int damage, float cooldown, int checkAnim);

	public RightHandedLight rhLight;
	public LeftHandedLight lhLight;
	public RightHandedHeavy rhHeavy;
	public LeftHandedHeavy lhHeavy;
	public ChargeAttack charge;

	public int damageRHL, damageRHH, damageLHL, damageLHH, chargeH;

	public float cooldownRHL, cooldownRHH, cooldownLHL, cooldownLHH, cooldownCharge;

	public float countTimer;
	public float maxTimer;

	public Animator anim;
	
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

		/*if(getInputTriggers < 0){
			if(rhHeavy != null){
				rhHeavy (damageRHH, cooldownRHH, 3);
			}
		}*/
		/*else if(getInputTriggers > 0){
			if(lhHeavy != null){
				lhHeavy (damageLHH, cooldownLHH);
			}
		}*/

		if(Input.GetButtonDown("LB")){
			if(lhLight != null){
				lhLight (damageLHL, cooldownLHL, 1);
			}
		}

		if(Input.GetButtonDown("RB")){
			if(rhLight != null){
				rhLight (damageRHL, cooldownRHL, 2);
			}
		}

		if(getInputCharge == true && anim.GetBool("Efist")){
			if(charge != null){
				ChargeTimer();
				anim.SetBool("Charge", true);
			}
		}
		else{
			anim.SetBool("Charge", false);
			countTimer = 0;
		}
	}

	public void ChargeTimer (){

		countTimer += 1.0f * Time.deltaTime;

		if(countTimer > maxTimer){
			charge (chargeH, cooldownCharge, 3);
			countTimer = 0;
		}
	}
}


