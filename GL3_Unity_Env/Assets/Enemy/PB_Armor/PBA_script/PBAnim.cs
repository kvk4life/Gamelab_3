using UnityEngine;
using System.Collections;

public class PBAnim : MonoBehaviour {
	#region public vars
	public Animator anim;
	public bool gotWeapon = false;

	public GameObject Barbell;
	#endregion

	private int a1;
	private bool runOnce = true;
	private float currentLayerWeight;
	private float currentLayerWeight2;

	public AnimationClip clipThrow;		//Throw01_PBA
	public AnimationClip clipPickUp;	//Idle_weapom03_PBA
	private int clipIDThrow;
	private int clipIDTPickUp;
	public  AnimationEvent eventThrow;
	public AnimationEvent eventPickUp;

	public bool barBellEnable;
	public bool unLoopIdle;


	public void Start ()
	{
		anim = GetComponent<Animator>();
		currentLayerWeight = anim.GetLayerWeight (1);
		currentLayerWeight2 = anim.GetLayerWeight (2);
		Barbell = transform.Find ("PB weapon").gameObject;

		for (int i = 0; i < anim.runtimeAnimatorController.animationClips.Length; i++)
		{
			if (anim.runtimeAnimatorController.animationClips [i] == clipPickUp) 
			{
				clipIDTPickUp = i;
			}
			if (anim.runtimeAnimatorController.animationClips [i] == clipThrow) 
			{
				clipIDThrow = i;
			}
		}
		float clipLenght1 = clipThrow.length;

		eventThrow.time = clipLenght1 / 100f * 50.1f;
		eventThrow.functionName = "EnableBarbell";
		clipThrow.AddEvent (eventThrow);

		float clipLenght2 = clipPickUp.length;

		eventPickUp.time = clipLenght2 / 100f * 27.1f;
		eventPickUp.functionName = "DisableBarbell";
		clipPickUp.AddEvent (eventPickUp);

	}

	public void EnableBarbell()
	{
		Barbell.SetActive (false);
		unLoopIdle = false;
	}

	public void DisableBarbell()
	{
		if (!unLoopIdle) 
		{
			Barbell.SetActive (true);
			unLoopIdle = true;
		} 
	}

	#region Run Unequip Animatie
	public void RunNoWeaponFix()
	{
		if (anim.GetBool ("Run") && !gotWeapon)
		{
			anim.SetBool ("*RunUnEq", true);
		} 
		else
		{
			anim.SetBool ("*RunUnEq", false);
		}
		if (anim.GetBool ("*RunUnEq")) 
		{
			currentLayerWeight = Mathf.Lerp (currentLayerWeight, 1.0f, Time.deltaTime * 6);
			anim.SetLayerWeight (1, currentLayerWeight);
		}
		else 
		{
			currentLayerWeight = Mathf.Lerp (currentLayerWeight, 0.0f, Time.deltaTime * 6);
			anim.SetLayerWeight (1, currentLayerWeight);
		}
	}
    #endregion

    #region Attacku!
    public void Attack() {
        anim.SetTrigger("Attack");
        anim.SetBool("Run", false);
        a1 = Random.Range(0, 4);
        if (a1 < 2)
        {
            anim.SetFloat("BlendAttack", 0.0f);
        }
        else
        {
            anim.SetFloat("BlendAttack", 1.0f);
        }
    }
	#endregion

	#region PickUPUU!!
	public void pickUpWeapon()
	{
		if (anim.GetBool ("PickUp")) 
		{
			gotWeapon = true;
			anim.SetBool ("Run", false);
			anim.SetBool ("*HasWeapon", true);
		}
		if (anim.GetBool ("Throw")) 
		{
			gotWeapon = false;
			anim.SetBool ("*HasWeapon", false);
		}
	}
	#endregion

	#region Throw
	public void Throw ()
	{
		if (anim.GetBool ("Throw")) 
		{
			anim.SetBool ("Run", false);
		}
	}
	#endregion

	#region GetHitNoWeapon Animation
	//Animatie Layer 2. zonder wapen in handen
	public void GetHitNoWeapon ()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Get Hit") && !gotWeapon) 
		{
			Barbell.SetActive (false);
			anim.SetLayerWeight (2, 1);
		} 
		else 
		{
			currentLayerWeight2 = Mathf.Lerp (currentLayerWeight2, 0.0f, Time.deltaTime * 6);
			anim.SetLayerWeight (2, currentLayerWeight2);
		}
	}
	#endregion

	public void Update () 
	{
		RunNoWeaponFix ();
		GetHitNoWeapon ();

		pickUpWeapon ();
	//	Throw ();

	//	WeaponOff ();
	}
}

