using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PBAragdollController : MonoBehaviour {
	public Component[] bones;
	public int boneCount;
	public Animator anim;
	public bool isDead;
	//reset parentz
	private GameObject spine;
	private GameObject ring03;
	private GameObject spine02;
	private GameObject ring02;
	private GameObject ring01;
	//destructable Helm
	private GameObject Helm;
	private int randomPart;
	public int helmCount;
	public GameObject[] helmParts;
    public List<GameObject> helmPartz = new List<GameObject>();

	void Start () {

		anim = GetComponent<Animator> ();

		boneCount = gameObject.GetComponentsInChildren<Rigidbody> ().Length; 
		RunBoneCount ();

		foreach (Rigidbody ragdoll in bones)
		{
			ragdoll.isKinematic = true;
		}

		#region Attach Rings
		ring03 = transform.Find ("PB Armor/AttachArmorPoint03").gameObject;
		spine = transform.Find ("PB_Main_Rot/Dummy001/Bip001/Bip001 Pelvis/Bip001 Spine").gameObject;
		ring02 = transform.Find ("PB Armor/AttachArmorPoint02").gameObject;
		ring01 = transform.Find ("PB Armor/AttachArmorPoint01").gameObject;
		spine02 = transform.Find ("PB_Main_Rot/Dummy001/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2").gameObject;
		#endregion
		Helm = transform.Find ("PB_Main_Rot/Dummy001/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Spine3/Bip001 Neck/Bip001 Head/AttachHelm02").gameObject;
	}

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            print("Doet jump het nou?");
            HeavyDamage();
        }
    }

	void RunBoneCount()
	{
		bones = new Component[boneCount];
		bones = gameObject.GetComponentsInChildren<Rigidbody>(); 
	}

	public void killRagdoll () 
	{
        ring01.transform.parent = spine02.transform;
        ring02.transform.parent = spine02.transform;
        ring03.transform.parent = spine.transform;
        foreach (Rigidbody ragdoll in bones)
		{
			
			ragdoll.isKinematic = false;
		}
		anim.enabled = false;
	}

	#region HelmBreak
	public void HeavyDamage()
	{
        if (helmPartz.Count > 0) {
            int randomHelmPart = Random.Range(0, helmPartz.Count);        
            helmPartz[randomHelmPart].GetComponent<Rigidbody>().isKinematic = false;
            helmPartz[randomHelmPart].transform.parent = null;
            StartCoroutine(Wait4Destroy(helmPartz[randomHelmPart]));
            helmPartz.RemoveAt(randomHelmPart);
        }
    }

	IEnumerator Wait4Destroy(GameObject helmPartToDestroy)
	{
		yield return new WaitForSeconds (2f);
		Destroy (helmPartToDestroy);
	}
	#endregion
}