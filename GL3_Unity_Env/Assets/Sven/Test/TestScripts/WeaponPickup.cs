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

	void Start () {
	
	}
	
	void Update () {
	
		WeaponSwitch ();
		PickUp ();

	}

	public void PickUp () {

		if(Input.GetButtonDown("X")){
			if(Physics.Raycast(transform.position, transform.forward, out hit, distance)){
				if(hit.transform.tag == weaponTag){
					StoreWeapon(hit.transform.gameObject);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}

	public void StoreWeapon (GameObject storeWeapon) {

		print(storeWeapon);

		currentWeapon = storeWeapon;

	}

	public void WeaponSwitch () {

		if(Input.GetButtonDown("DpadLeft")){

			currentWeapon = weapons[0];

		}

		if(Input.GetButtonDown("DpadRight")){

			currentWeapon = weapons[1];

		}
	}
}
