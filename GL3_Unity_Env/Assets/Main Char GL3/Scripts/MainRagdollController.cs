using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainRagdollController : MonoBehaviour {

    KnockDown kO;

    [HideInInspector]
    public Animator anim;

    private Component[] bones;
    private Collider[] bnColliders;

    private int boneCount;

    public bool activateRag;


	void Start () {

        kO = GetComponent<KnockDown>();

		anim = GetComponent<Animator> ();

		boneCount = gameObject.GetComponentsInChildren<Rigidbody> ().Length; 
		RunBoneCount ();

		foreach (Rigidbody ragdoll in bones)
		{
			ragdoll.isKinematic = true;
		}

        bnColliders = gameObject.GetComponentsInChildren<Collider>();
	}

	void RunBoneCount()
	{
		bones = new Component[boneCount];
		bones = gameObject.GetComponentsInChildren<Rigidbody>(); 
	}

	void killRagdoll () 
	{
        
            foreach (Rigidbody ragdoll in bones)
            {
                ragdoll.isKinematic = false;
            }
        
		anim.enabled = false;
	}

    void RagdollOff()
    {

            foreach (Rigidbody ragdoll in bones)
            {

                ragdoll.isKinematic = true;
            }
        
        anim.enabled = true;

    }



    void FixedUpdate ()
	{
        if (!kO.getUpActive && !kO.setKinematic) //niet bestuurbaar als KnockDownScript zn ding doet
        {
            if (activateRag)
            {
                killRagdoll();
                          
            }
            else
            {
                RagdollOff();
            }
        }
	}

}