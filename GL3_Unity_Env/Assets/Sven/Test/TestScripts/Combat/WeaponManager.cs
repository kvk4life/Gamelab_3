using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	public int weaponDamageH;
	public int weaponCooldownH;
	public int weaponDamageL;
	public int weaponCooldownL;

	public DelegateWeapons delegateWeapons;

	void Start () {

		delegateWeapons = GameObject.Find("Adolf").GetComponent<DelegateWeapons>();
		SwitchStats ();
		
	
	} 
	
	void Update () {
	
		
	
	}

	public void SwitchStats () {

		delegateWeapons.damageRHL = weaponDamageH;
		delegateWeapons.cooldownRHL = weaponCooldownH;
		delegateWeapons.damageLHL = weaponDamageL;
		delegateWeapons.cooldownLHL = weaponCooldownL;

	}
}
