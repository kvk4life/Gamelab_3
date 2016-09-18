using UnityEngine;
using System.Collections;

public abstract class Base : MonoBehaviour {
	public string myTeam;
	public int curHealth;
	public int maxHealth;

	public abstract void Attack (GameObject myTarget);

	public abstract void TargetSelect ();

	public abstract void RecieveDamage (int recievedDamage);

	public abstract void HealthChecker ();

	public abstract void Death ();
}
