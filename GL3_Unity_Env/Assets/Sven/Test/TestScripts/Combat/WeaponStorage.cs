using UnityEngine;
using System.Collections;

//Sven Evertse ©

public class WeaponStorage : MonoBehaviour {

	public GameObject [] weaponStorage;
	public GameObject curWeapon;
	public GameObject spawn;

	public Transform weaponSpawn;

	public string  wantedWeapon;

	void Start () {
	
		GetWeapons();
		GetComponent<WeaponPickup>().StoreWeapon(0);
		GetComponent<WeaponPickup>().Switcher(0);

	}
	
	void Update () {

	}

	public void GetWeapons (){


		for(int i = 0; i < weaponStorage.Length; i ++){
			if(wantedWeapon == weaponStorage[i].name){
				curWeapon = Resources.Load(weaponStorage[i].name) as GameObject;
				spawn = Instantiate(curWeapon, weaponSpawn.position, curWeapon.transform.rotation) as GameObject;
				spawn.transform.SetParent(weaponSpawn);
				GetComponent<Combat>().attackCollision = spawn;
			}
		}
	}
}
