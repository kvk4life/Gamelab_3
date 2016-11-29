using UnityEngine;
using System.Collections;



public class ArmorDamage : MonoBehaviour {

	public GameObject Helm02;
	public Animator PBanim;

	// Use this for initialization
	void Start () 
	{
		Helm02 = transform.Find ("PB_Main_Rot/Dummy001/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Spine3/Bip001 Neck/Bip001 Head/AttachHelm02/Pig Benis Helm L 02").gameObject;
		PBanim = GetComponent<Animator> ();
		//	piecie = transform.Find ("PB_Main_Rot/Dummy001/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Spine3/Bip001 Neck/Bip001 Head/AttachHelm01/Pig Benis Helm L 02").gameObject;
	//	Helm02.transform.parent = null;
	//	PBanim.Rebind ();
		//Destroy (Helm02);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
