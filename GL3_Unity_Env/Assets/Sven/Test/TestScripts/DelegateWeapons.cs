using UnityEngine;
using System.Collections;

public class DelegateWeapons : MonoBehaviour {

	public delegate void RightHandedLight ();
	public delegate void LeftHandedLight ();

	public RightHandedLight rhLight;
	public LeftHandedLight lhLight;

	void Start () {
	
	}
	
	void Update () {
		
		TriggerDetect();

	}

	public void TriggerDetect (){

		float getInput;
		getInput = Input.GetAxis("Triggers");

		if(getInput < 0){
			if(rhLight != null){
				rhLight();
			}
		}
		else if(getInput > 0){
			if(lhLight != null){
				lhLight();
			}
		}
	}
}


