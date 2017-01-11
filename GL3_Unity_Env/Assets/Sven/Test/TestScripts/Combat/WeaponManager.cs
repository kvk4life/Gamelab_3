using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	public int weaponDamage;
	public int weaponCooldown;

	public DelegateWeapons delegateWeapons;

	void Start () {

		delegateWeapons = GameObject.Find("AdolfFunctionaliteiten").GetComponent<DelegateWeapons>();
		SwitchStats ();
		
	
	} 
	
	void Update () {
	
	}

	public void SwitchStats () {

		delegateWeapons.damageRHL = weaponDamage;
		delegateWeapons.cooldownRHL = weaponCooldown;

	}
}
