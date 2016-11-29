using UnityEngine;
using System.Collections;

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
	public bool armorDamage;
	private GameObject Helm;
	private int randomPart = 0;
	public int helmCount = 0;
	public GameObject[] helmParts;

	public bool deleteH = false;


	void Start () {

		anim = GetComponent<Animator> ();
		//killScript = GetComponent<smileyDeath> ();

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
		helmParts = new GameObject[Helm.transform.childCount];
		for (int i = 0; i < Helm.transform.childCount; i++) 
		{
			helmParts [i] = Helm.transform.GetChild (i).gameObject;
			helmCount = i;
		}
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

	#region HelmBreak
	void HeavyDamage()
	{
		if (armorDamage)
		{
			randomPart = Random.Range (0, 5);
			if (helmParts [randomPart] != null) {
	//			print (randomPart);
				helmParts [randomPart].GetComponent<Rigidbody> ().isKinematic = false;
				helmParts [randomPart].transform.parent = null;
				helmParts [randomPart] = null;

				RunBoneCount ();
				StartCoroutine ("Wait4Destroy");
			}
			else if (helmParts [randomPart] == null) 
			{
				randomPart = Random.Range (0, 5);
	//			print ("!" + randomPart);
				CheckAllHelmParts ();
			}
		}
	}

	void CheckAllHelmParts()
	{
		for (int i = 0; i < helmCount; i++) 
		{
			if (helmParts [i] != null) {
				deleteH = false;
			} 
			else 
			{
				print ("kont KAPOT SWAAA!");
				deleteH = true;
				armorDamage = false;
				return;
			}
		}
	}

	IEnumerator Wait4Destroy()
	{
		armorDamage = false;
		yield return new WaitForSeconds (3f);
		Destroy (helmParts [randomPart]);
	}
	#endregion

	void FixedUpdate ()
	{
		if (isDead) 
		{
			ring01.transform.parent = spine02.transform;
			ring02.transform.parent = spine02.transform;
			ring03.transform.parent = spine.transform;
			killRagdoll ();
		}
	}

	void LateUpdate()
	{
		HeavyDamage ();
	}
}