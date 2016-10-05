using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : Base {
	public enum TowerState
	{
		Hitable,
		Protected,
		Dead,
		ProtectPlayer
	}
	public TowerState myState;
	public TowerState previousState;
	private Stats statsClass;
	public List<GameObject> targetList = new List<GameObject> ();
	public List<GameObject> allyList = new List<GameObject> ();
    public Transform myProjectileHolder;
	public GameObject projectilePool;
	public GameObject enemyPlayer;
	private GameObject curTarget;
	public Animator myAnim;
	public AnimationClip myClip;
	public AnimationEvent myEvent;
    public int myClipInt;
    public float eventTimerPercentage;
	public float attackRate;
	private float nextAttack;
	public string playerTag;

	void Start(){
		statsClass = GetComponent<Stats>();
		myAnim = GetComponent<Animator> ();
        myClip = myAnim.runtimeAnimatorController.animationClips[myClipInt];
        float clipLength = myClip.length;
        myEvent.time = clipLength / 100 * eventTimerPercentage;
        myEvent.functionName = "ActivatePool";
        myClip.AddEvent(myEvent);
	}

	public void OnTriggerEnter(Collider col){
		if (col.GetComponent<Stats> () != null) {
			if (col.GetComponent<Stats> ().teamNumber != statsClass.teamNumber) {
				targetList.Add (col.gameObject);
			}
			else if (col.transform.tag == playerTag) {
				GameObject ally = col.gameObject;
				ally.GetComponent<Stats> ().insideTurret = true;
				ally.GetComponent<Stats> ().turretIAmIn = gameObject;
				allyList.Add (ally);
			}
		}
	}

	public void OnTriggerExit(Collider col){
		if (col.GetComponent<Stats> () != null) {
			if (col.GetComponent<Stats> ().teamNumber != statsClass.teamNumber) {
				if(enemyPlayer != null && col.gameObject == enemyPlayer){
					enemyPlayer = null;
				}
				for (int e = 0; e < targetList.Count; e++) {
					if (col.gameObject == targetList [e]) {
						targetList.Remove (col.gameObject);
						break;
					}
				}
			}
			else {
				for (int e = 0; e < allyList.Count; e++) {
					if (col.gameObject == allyList [e]) {
						GameObject ally = col.gameObject;
						ally.GetComponent<Stats> ().insideTurret = false;
						ally.GetComponent<Stats> ().turretIAmIn = null;
						allyList.Remove (ally);
						break;
					}
				}
			}
		}
	}

	public void Update(){
		StateChecker ();
	}
		
	public void StateChecker(){
		switch (myState) {
		case TowerState.Hitable:
		case TowerState.Protected:
			previousState = myState;
			TargetSelect ();
			break;	
		case TowerState.ProtectPlayer:
			TargetSelect ();
			break;
		}
	}
		
	public void ProtectPlayer(GameObject recievedTarget){
		if (enemyPlayer == null) {
			for (int i = 0; i < targetList.Count; i++) {
				if(targetList[i] == recievedTarget){
					enemyPlayer = recievedTarget;
					myState = TowerState.ProtectPlayer;
					break;
				}
			}
		}
	}

	public override void TargetSelect(){
		if(myState == TowerState.ProtectPlayer){
			if (enemyPlayer != null && !enemyPlayer.GetComponent<Stats> ().dead) {
				Attack (enemyPlayer);
			}
			else {
				enemyPlayer = null;
				myState = previousState;
			}
		}
		else{
			if (targetList.Count > 0) {
				if (targetList [0] != null) {
					Attack (targetList [0]);
				}
				else {
					for(int i = 0; i < targetList.Count; i++){
						if (targetList [i] == null) {
							targetList.RemoveAt (i);
						} 
					}
				}
			}
		}
	}

	public override void Attack(GameObject myTarget){
		if(Time.time > nextAttack){
			myAnim.SetBool ("attack", true);
            curTarget = myTarget;
			nextAttack = Time.time + attackRate;
		}
	}

	public void ActivatePool(){
        projectilePool.GetComponent<PoolMngProjectile>().ActivatePool(curTarget, myProjectileHolder);
        myAnim.SetBool("attack", false);
    }

	public override void RecieveDamage(int recievedDamage){
		//Dit moet later uitgebreid worden met de damage stats enzo
		curHealth -= recievedDamage;
		HealthChecker ();
	}

	public override void HealthChecker(){
		if(curHealth < 1){
			curHealth = 0;
			Death ();
		}
	}

	public override void Death(){
		myState = TowerState.Dead;
		//Ik moet nog zien hoe kilian dit in elkaar gaat zettem
	}
}
