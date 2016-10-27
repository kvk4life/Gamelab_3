using UnityEngine;
using System.Collections;

public class WeaponStorage : MonoBehaviour {

	public GameObject [] weaponStorage;
	public GameObject curWeapon;

	public string  wantedWeapon;

	void Start () {
	
	}
	
	void Update () {

	}

	public void GetWeapons (){

		for(int i = 0; i < weaponStorage.Length; i ++){
			if(wantedWeapon == weaponStorage[i].name){
				curWeapon = Resources.Load(weaponStorage[i].name) as GameObject;
			}
		}
	}
}
