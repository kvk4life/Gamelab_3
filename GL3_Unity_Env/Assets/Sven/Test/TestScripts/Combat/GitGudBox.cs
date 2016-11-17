using UnityEngine;
using System.Collections;

public class GitGudBox : MonoBehaviour {

	public GameObject [] weapons;

	public int randomWeapon;
	public int finalWeapon;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void SetWeapon (bool setGun) {

		if(setGun == true){
			randomWeapon = Random.Range(0, weapons.Length);
		}

	}


	IEnumerator CheckWeapon (float cooldown) {
		
		SetWeapon(true);
		print(randomWeapon);
		yield return new WaitForSeconds(cooldown);
		SetWeapon(false);

	}
}
