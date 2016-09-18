using UnityEngine;
using System.Collections;

public class TestPlayer : Base {
	//Wanneer klaar met de Tower script, VERWIJDER dit script dan!
	// Use this for initialization
	public bool insideTurret;
	public bool attackedByPlayer;
	public bool dead;
	public GameObject playerAttackedBy;
	public GameObject turretIAmIn;

	public void Update(){
		if(insideTurret && attackedByPlayer){
			turretIAmIn.GetComponent<Tower> ().ProtectPlayer (playerAttackedBy);
		}
	}

	public override void Attack(GameObject myTarget){

	}

	public override void TargetSelect(){

	}

	public override void RecieveDamage(int recievedDamage){

	}

	public override void HealthChecker(){

	}

	public override void Death(){

	}
}
