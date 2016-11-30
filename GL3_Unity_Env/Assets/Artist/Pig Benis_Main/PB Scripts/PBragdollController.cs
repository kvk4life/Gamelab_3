using UnityEngine;
using System.Collections;

public class PBragdollController : MonoBehaviour {

	public Component[] bones;
	public int boneCount;
	public Animator anim;
	public bool isDead;

	//public smileyDeath killScript;



	void Start () {

		anim = GetComponent<Animator> ();

		//killScript = GetComponent<smileyDeath> ();

		boneCount = gameObject.GetComponentsInChildren<Rigidbody> ().Length; 
		bones = new Component[boneCount];
		bones = gameObject.GetComponentsInChildren<Rigidbody> (); 

		foreach (Rigidbody ragdoll in bones)
		{
			ragdoll.isKinematic = true;
		}
	}

	void killRagdoll () 
	{

		foreach (Rigidbody ragdoll in bones)
		{
			
			ragdoll.isKinematic = false;
		}
		anim.enabled = false;
	}



	void Update ()
	{
		if (isDead) 
		{
			killRagdoll ();
		}
	}
}