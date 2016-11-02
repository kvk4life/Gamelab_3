using UnityEngine;
using System.Collections;

//Sven Evertse ©

public class WeaponPickup : MonoBehaviour {

	public GameObject [] weapons;
	public GameObject currentWeapon;
	public GameObject camera;

	public RaycastHit hit;

	public float distance;

	public string weaponTag;

	WeaponStorage weaponStorage;

	void Start () {
		
		weaponStorage = GetComponent<WeaponStorage>();

	}
	
	void Update () {
	
		WeaponSwitch ();
		PickUp ();

	}

	public void PickUp () {

		if(Input.GetButtonDown("X")){
			if(Physics.Raycast(transform.position, transform.forward, out hit, distance)){
				if(hit.transform.tag == weaponTag){
					weaponStorage.GetWeapons();
					StoreWeapon(hit.transform.gameObject, 1);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}

	public void StoreWeapon (GameObject storeWeapon, int nullChecker) {

		weaponStorage.wantedWeapon = storeWeapon.name;

		if(weapons[nullChecker] == null){
			weapons[nullChecker] = weaponStorage.spawn;
		}
		else{
			for(int i = 0; i < weapons.Length; i ++){
				if(currentWeapon.name == weapons[i].name){
					weapons[i] = weaponStorage.spawn;
				}
			}
		}
	}

	public void WeaponSwitch () {

		if(Input.GetAxis("DpadHor") < 0){
			print(0);

			Switcher(0);

		}

		if(Input.GetAxis("DpadHor") > 0){
			print(1);

			Switcher(1);

		}
	}

	public void Switcher (int switcher) {

		switch (switcher){

			case 0:

				currentWeapon = weapons[0];
				currentWeapon.SetActive(true);

			break;

			case 1:

				currentWeapon = weapons[1];
				currentWeapon.SetActive(true);

			break;
		}
	}
}
