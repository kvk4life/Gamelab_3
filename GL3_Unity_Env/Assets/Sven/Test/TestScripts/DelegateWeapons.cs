using UnityEngine;
using System.Collections;

public class DelegateWeapons : MonoBehaviour {

	public delegate void RightHandedLight (int damage, float cooldown);
	public delegate void LeftHandedLight (int damage, float cooldown);
	public delegate void RightHandedHeavy (int damage, float cooldown);
	public delegate void LeftHandedHeavy (int damage, float cooldown);
	public delegate void ChargeAttack (int damage, float cooldown);

	public RightHandedLight rhLight;
	public LeftHandedLight lhLight;
	public RightHandedHeavy rhHeavy;
	public LeftHandedHeavy lhHeavy;
	public ChargeAttack charge;

	public int damageRHL, damageRHH, damageLHL, damageLHH, chargeH;

	public float cooldownRHL, cooldownRHH, cooldownLHL, cooldownLHH, cooldownCharge;

	public float countTimer;
	public float maxTimer;

	void Start () {
	
	}
	
	void Update () {
		
		TriggerDetect();

	}

	public void TriggerDetect (){

		bool mayCharge;

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

		if(getInputCharge == true && mayCharge == true){
			if(charge != null){
				print("LOL1");
				ChargeTimer();
			}
		}
	}

	public void ChargeTimer (){

		countTimer += 1.0f * Time.deltaTime;

		if(countTimer > maxTimer){
			print("LOL2");
			mayCharge = false;
			charge (chargeH, cooldownCharge);
			countTimer = 0;
		}
	}
}


