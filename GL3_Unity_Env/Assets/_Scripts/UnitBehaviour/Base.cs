using UnityEngine;
using System.Collections;

public abstract class Base : MonoBehaviour {
	public string myTeam;

	public abstract void Attack (GameObject myTarget);

	public abstract void TargetSelect ();

	public abstract void HealthChecker ();

	public abstract void Death ();
}
