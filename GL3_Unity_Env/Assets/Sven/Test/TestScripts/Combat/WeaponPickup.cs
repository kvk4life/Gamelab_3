﻿using UnityEngine;
using System.Collections;

//Sven Evertse ©

public class WeaponPickup : MonoBehaviour {

	public GameObject [] weapons;
	public GameObject currentWeapon;
	public GameObject camera;
	public GameObject player;

	public Animator anim;

	public RaycastHit hit;

	public float distance;
	public float gitGudBoxCount;

	public string weaponTag;

	public int switchCount;

	WeaponStorage weaponStorage;

	public GitGudBox ggBox;

	void Start () {
	
		player.GetComponent<Combat>().attackCollision = weapons[0];

	}
	
	void Update () {
	
		WeaponSwitch ();
		PickUp ();
		ActivateGitGud ();

		if(anim.GetBool("Efist")){

			currentWeapon.SetActive(false);

		}
		else{

			currentWeapon.SetActive(true);

		}

	}

	public void PickUp () {

		if(Input.GetButtonDown("X")){
			if(Physics.Raycast(transform.position, transform.forward, out hit, distance)){
				weaponStorage.wantedWeapon = hit.transform.name;
				if(hit.transform.tag == weaponTag){
					weaponStorage.GetWeapons();
					StoreWeapon(1);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}

	public void StoreWeapon (int nullChecker) {

		weaponStorage = GetComponent<WeaponStorage>();

		if(weapons[nullChecker] == null){
			weapons[nullChecker] = weaponStorage.spawn;
		}
		else{
			for(int i = 0; i < weapons.Length; i ++){
				if(switchCount == i){
					Destroy(currentWeapon);
				}
				if(currentWeapon.name == weapons[i].name){
					weapons[i] = weaponStorage.spawn;
				}
			}
		}

		Switcher(switchCount);
		
	}

	public void WeaponSwitch () {

		if(Input.GetAxis("DpadHor") < 0){

			switchCount = 0;
			Switcher(switchCount);

		}

		if(Input.GetAxis("DpadHor") > 0){

			switchCount = 1;
			Switcher(switchCount);

		}
	}

	public void Switcher (int switcher) {

		switch (switcher){

			case 0:

				if(weapons[switcher] != null){

					currentWeapon = weapons[switcher];
					currentWeapon.SetActive(true);
					weapons[switcher].GetComponent<WeaponManager>().SwitchStats();
					player.GetComponent<Combat>().attackCollision = weapons[switcher];


					if(weapons[1]  != null){
						weapons[1].SetActive(false);
					}
				}

			break;

			case 1:

				if(weapons[switcher] != null){

					currentWeapon = weapons[switcher];
					currentWeapon.SetActive(true);
					weapons[switcher].GetComponent<WeaponManager>().SwitchStats();
					player.GetComponent<Combat>().attackCollision = weapons[switcher];

					if(weapons[0]  != null){
						weapons[0].SetActive(false);
					}
				}

			break;
		}
	}

	public void ActivateGitGud () {

		if(Input.GetButtonDown("X")){
			if(Physics.Raycast(transform.position, transform.forward, out hit, distance)){
				if(hit.transform.tag == "GitGudBox"){
					ggBox = hit.transform.gameObject.GetComponent<GitGudBox>();
					ggBox.StartCoroutine("CheckWeapon", gitGudBoxCount);
				}
			}
		}
	}
}
