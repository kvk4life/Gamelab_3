using UnityEngine;
using System.Collections;

public class PBAnim : MonoBehaviour {
	#region public vars
	public Animator anim;
	public bool gotWeapon = false;
    public GameObject barbellBone;
	public GameObject barbell;
    public Transform pickUpHand;
    public Transform barbellTransitionPoint;
    private Coroutine barbellCoroutine;
	#endregion

	private int a1;
	private bool runOnce = true;
	private float currentLayerWeight;
	private float currentLayerWeight2;

	public AnimationClip clipThrow;		    //Throw01_PBA
	public AnimationClip clipIdleWeapon;	//Idle_weapom03_PBA
    public AnimationClip clipPickUp;        //pickUp weapon01_PBA
	/*private int clipIDThrow;
    private int clipIDIdleWeapon;
    private int clipIDPickUp; */
	public  AnimationEvent eventThrow;
	public AnimationEvent eventPickUp;
    public AnimationEvent eventIdleWeapon;

	public bool barBellEnable;
	public bool unLoopIdle;
    public bool followHand;


	public void Start ()
	{
		anim = GetComponent<Animator>();
		currentLayerWeight = anim.GetLayerWeight (1);
		currentLayerWeight2 = anim.GetLayerWeight (2);
        SetTheEvents();
	}
    void SetTheEvents() {
        float clipLenght1 = clipThrow.length;
        eventThrow.time = clipLenght1 / 100f * 50.1f;
        eventThrow.functionName = "EnableBarbell";
        clipThrow.AddEvent(eventThrow);

        float clipLenght2 = clipIdleWeapon.length;
        eventIdleWeapon.time = clipLenght2 / 100f * 27.1f;
        eventIdleWeapon.functionName = "DisableBarbell";
        clipIdleWeapon.AddEvent(eventIdleWeapon);

        float clipLenght3 = clipPickUp.length;
        eventPickUp.time = clipLenght3 / 100f * 53.9f;
        eventPickUp.functionName = "FollowHandEvent";
        clipPickUp.AddEvent(eventPickUp);
    }

    /*void SearchClipID() {
        for (int i = 0; i < anim.runtimeAnimatorController.animationClips.Length; i++)
        {
            if (anim.runtimeAnimatorController.animationClips[i] == clipIdleWeapon)
            {
                clipIDIdleWeapon = i;
            }
            if (anim.runtimeAnimatorController.animationClips[i] == clipThrow)
            {
                clipIDThrow = i;
            }
            if (anim.runtimeAnimatorController.animationClips[i] == clipPickUp)
            {
                clipIDPickUp = i;
            }
        }
    }*/

	public void EnableBarbell()
	{
        barbell.SetActive (false);
		unLoopIdle = false;
	}

	public void DisableBarbell()
	{
		if (!unLoopIdle) 
		{
            followHand = false;
            barbell.GetComponent<BarbellCtrl>().ObtainBarbell(barbellBone);
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

	#region PickUp
	public void PickUpWeapon() {
        anim.SetTrigger("PickUp");
        gotWeapon = true;
        anim.SetBool("Run", false);
        anim.SetBool("*HasWeapon", true);
    }

    public void FollowHandEvent() {
        followHand = true;
    }

    void ConnectBarbellToHand() {
        if (followHand) {
            barbell.transform.position = new Vector3(barbell.transform.position.x, pickUpHand.position.y, pickUpHand.position.z);
            //if (barbellCoroutine == null) {
                //float rotationDuration = clipPickUp.length - eventPickUp.time;
                //float anglesToRotate = Quaternion.Angle(barbell.transform.rotation, barbellTransitionPoint.rotation);
              //  barbellCoroutine = StartCoroutine(RotateBarbell(Vector3.up * anglesToRotate, rotationDuration));
            //}
        }
    }

    IEnumerator RotateBarbell(Vector3 byAngles, float inTime) {
        Transform barbellTrans = barbell.transform;
        var fromAngle = barbellTrans.rotation;
        var toAngle = Quaternion.Euler(barbellTrans.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime) {
            barbellTrans.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            print("Barbell is rotating");
            yield return null;
        }
    }
    #endregion

    #region Throw
    public void Throw ()
	{
		if (anim.GetBool ("Throw")) 
		{
            gotWeapon = false;
            anim.SetBool ("Run", false);
            anim.SetBool("*HasWeapon", false);
        }
	}
	#endregion

	#region GetHitNoWeapon Animation
	//Animatie Layer 2. zonder wapen in handen
	public void GetHitNoWeapon ()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Get Hit") && !gotWeapon) 
		{
			barbell.SetActive (false);
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
        ConnectBarbellToHand();
        if (Input.GetButtonDown("Jump")) {
            PickUpWeapon();
        }
	//	PickUpWeapon ();
	//	Throw ();
	//	WeaponOff ();
	}
}

