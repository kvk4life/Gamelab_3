using UnityEngine;
using System.Collections;

public class DelegateWeapons : MonoBehaviour {

	public bool mayAttack;

	public delegate void RightHandedLight (int num);

	RightHandedLight rhLight;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void TriggerDetect (){

		float getInput;
		getInput = Input.GetAxis("Triggers");

		if(getInput < 0){
			if(rhLight != null){
				
			}
		}
		else if(getInput > 0){
			
		}
	}
}


