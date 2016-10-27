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

		Switcher(0);

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
					StoreWeapon(hit.transform.gameObject);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}

	public void StoreWeapon (GameObject storeWeapon) {

		weaponStorage.wantedWeapon = storeWeapon.name;

		if(weapons[1] == null){
			weapons[1] = weaponStorage.curWeapon;
		}
		else{
			for(int i = 0; i < weapons.Length; i ++){
				if(currentWeapon.name == weapons[i].name){
					weapons[i] = weaponStorage.curWeapon;
				}
			}
		}
	}

	void WeaponSwitch () {

		if(Input.GetButtonDown("DpadLeft")){

			Switcher(0);

		}

		if(Input.GetButtonDown("DpadRight")){

			Switcher(1);

		}
	}

	void Switcher (int switcher) {

		switch (switcher){

			case 0:

				currentWeapon = weapons[0];

			break;

			case 1:

				currentWeapon = weapons[1];

			break;
		}
	}
}
