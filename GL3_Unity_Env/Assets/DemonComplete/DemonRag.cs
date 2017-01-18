using UnityEngine;
using System.Collections;

public class DemonRag : MonoBehaviour {

	public Rigidbody[] bones;
	public Animator anim;

	public bool assRape = false;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();

		bones = GetComponentsInChildren<Rigidbody> ();

		foreach (Rigidbody rb in bones) 
		{
			rb.isKinematic = true;
		}
	
	}

	void RagActive()
	{
		anim.enabled = false;
		foreach (Rigidbody rb in bones) 
		{
			rb.isKinematic = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (assRape) 
		{
			RagActive ();
		}
	}
}
