using UnityEngine;
using System.Collections;

[System.Serializable]
abstract public class Weapon_Manager : MonoBehaviour {

	public bool mayFire;

	protected abstract void FireWeapon ();

	protected abstract void FillDelegate ();
		
	protected abstract void WeaponStats (int damage, float rPM, float range, float speed);

	protected abstract IEnumerator Cooldown (float cooldown);

}