using UnityEngine;
using System.Collections;

public class GitGudBox : MonoBehaviour {

	public GameObject [] weapons;

	public int randomWeapon;
	public int finalWeapon;

	public Transform ggBoxPos;

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

		SpawnWeapon ();
		
		SetWeapon(true);
		print(weapons[randomWeapon].name);
		yield return new WaitForSeconds(cooldown);
		SetWeapon(false);

	}

	public void SpawnWeapon (){

		Instantiate (weapons[randomWeapon], ggBoxPos.position, transform.rotation);

	}
}
