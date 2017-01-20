using UnityEngine;
using System.Collections;

public class MainAnim01 : MonoBehaviour
{
    //KnockDownTime moet je vanaf dit Script instellen

    MainRagdollController ragD;
    KnockDown kO;
    [HideInInspector]
    public Animator anim;

    private float moveSpeedZ; //MoveSpeed 0f = achteruit lopen, 1f = Idle, 2f = lopen, 3f = rennen (voor controller gebruik mathF.Lerp)
    private float idleRotate;

    private float baseLayerWeight;
    private float weaponLayerWeight;
    private float whileMoveLayerWeight; //Ctrl Animatie Layer voor het overriden van de wapen animaties
    private float leftArmLayerWeight; //Ctrl Animatie Layer Items etc gebruiken tijdens lopen en equipped zijn
    private float pickUpLayerWeight;

    public bool hasWeapon;
    public bool disableMoveLayer; // zet Layer 2 uit als bepaalde animaties spelen
    public bool LArmActive;
    public bool PickUpActive;
    public bool gotHit;
   
    public float knockDownTime;

    private AnimatorStateInfo currentWeaponState;
    private AnimatorStateInfo currentBaseState;
    private AnimatorStateInfo currentLArmState;
    private AnimatorStateInfo currentPickUpState;
    private AnimatorStateInfo currentAssKickState;


    void Start()
    {
        ragD = GetComponent<MainRagdollController>();
        kO = GetComponent<KnockDown>();
        anim = GetComponent<Animator>();

        baseLayerWeight = anim.GetLayerWeight(0);
        weaponLayerWeight = anim.GetLayerWeight(1);
        whileMoveLayerWeight = anim.GetLayerWeight(2);
        leftArmLayerWeight = anim.GetLayerWeight(3);
        pickUpLayerWeight = anim.GetLayerWeight(4);
    }

    #region Movement Shit
    void BasicMovement()
    {
        moveSpeedZ = anim.GetFloat("MoveSpeed");
        idleRotate = anim.GetFloat("MoveRot");

        //Als Adolf niet stil staat, staat rotatie Animatie uit
        if (moveSpeedZ != 1)
        {
            anim.SetFloat("MoveRot", 0);
        }
    }

    void MoveWhileEquip()
    {
        if (currentWeaponState.IsTag("FuckL2"))
        {
            disableMoveLayer = true;
        }
        else
        {
            disableMoveLayer = false;
        }

        if (hasWeapon && !disableMoveLayer && moveSpeedZ != 1f) 
        {
            whileMoveLayerWeight = Mathf.Lerp(whileMoveLayerWeight, 1, Time.deltaTime * 10);
            anim.SetLayerWeight(2, whileMoveLayerWeight);
        }
        else if(!currentBaseState.IsTag("FuckL2"))
        {
            whileMoveLayerWeight = Mathf.Lerp(whileMoveLayerWeight, 0, Time.deltaTime * 10);
            anim.SetLayerWeight(2, whileMoveLayerWeight);
        }

        if (moveSpeedZ == 1f && currentBaseState.IsTag("FuckL2")) //jump als player stilstaat(als WhileMovingLayer aanmoet ondanks moveSpeedZ op 1 staat)
        {
            //     disableMoveLayer = false;
            whileMoveLayerWeight = Mathf.Lerp(whileMoveLayerWeight, 1, Time.deltaTime * 10);
            anim.SetLayerWeight(2, whileMoveLayerWeight);

           
        }

        if (anim.GetFloat("MoveRot") > 0 || anim.GetBool("Strafe")) //als de speler vanuit Idle draait of strafe gaat Layer 2 aan
        {
            whileMoveLayerWeight = Mathf.Lerp(whileMoveLayerWeight, 1, Time.deltaTime * 100);
            anim.SetLayerWeight(2, whileMoveLayerWeight);

        }
    }


    #endregion

    #region Wapen Shit
    void WeaponSelect()
    {
        if (anim.GetBool("Efist") || anim.GetBool("E1hand") || anim.GetBool("E2hand"))
        {
            weaponLayerWeight = Mathf.Lerp(weaponLayerWeight, 1, Time.deltaTime * 10);
            anim.SetLayerWeight(1, weaponLayerWeight);

            hasWeapon = true;

        }
        else if (hasWeapon == true)
        {

            weaponLayerWeight = Mathf.Lerp(weaponLayerWeight, 0, Time.deltaTime * 10);
            anim.SetLayerWeight(1, weaponLayerWeight);

            StartCoroutine("waitHasWeaponF");
        }
    }

    IEnumerator waitHasWeaponF()
    {

        yield return new WaitForSeconds(0.38f); // wachtten tot dat weaponLayer op 0 staat

        if (!anim.GetBool("Efist") || !anim.GetBool("E1hand") || !anim.GetBool("E2hand"))
        {
            hasWeapon = false;
        }

    }

    void ChargeAttack()
    {
     /*   if (anim.GetBool("Ch Att"))
        {
            anim.SetBool("Charge", false);
        }*/
    }
    #endregion

    #region UseItem Shit
    void Larm()
    {
        if (currentLArmState.IsTag("LeftArm")) 
        {
            LArmActive = true;  
        }
        else
        {
            LArmActive = false;
        }

        if (LArmActive) 
        {
            leftArmLayerWeight = Mathf.Lerp(leftArmLayerWeight, 1, Time.deltaTime * 10);
            anim.SetLayerWeight(3, leftArmLayerWeight);
        }
        else
        {
            leftArmLayerWeight = Mathf.Lerp(leftArmLayerWeight, 0, Time.deltaTime * 10);
            anim.SetLayerWeight(3, leftArmLayerWeight);
        }
        // wanneer Pick Up Layer 2 aan
        if (currentPickUpState.IsTag("LeftArm"))
        {
            PickUpActive = true;
        }
        else
        {
            PickUpActive = false;
        }
        if (PickUpActive)
        {
            pickUpLayerWeight = Mathf.Lerp(pickUpLayerWeight, 1, Time.deltaTime * 15);
            anim.SetLayerWeight(4, pickUpLayerWeight);
        }
        else
        {
            pickUpLayerWeight = Mathf.Lerp(pickUpLayerWeight, 0, Time.deltaTime * 10);
            anim.SetLayerWeight(4, pickUpLayerWeight);
        }
    }

    #endregion

    #region Klappen Krijgen Shit
    void GetAssKicked()
    {
        if (currentAssKickState.IsTag("GetHit"))
        {
            gotHit = true;
        }
        else
        {
            gotHit = false;
        }

        if (gotHit || gotHit && moveSpeedZ == 1f)
        {
            whileMoveLayerWeight = Mathf.Lerp(whileMoveLayerWeight, 1, Time.deltaTime * 1000);
            anim.SetLayerWeight(2, whileMoveLayerWeight);
        }

    }

    void GetHitHard()
    {
        ragD.activateRag = true;
        StartCoroutine("JamaicaMan");

    }

    IEnumerator JamaicaMan()
    {

        yield return new WaitForSeconds(knockDownTime);

        kO.getUpActive = true;
        anim.SetBool("GetHitHard", false);
       
    }
    #endregion

    private void FixedUpdate()
    {
        currentWeaponState = anim.GetCurrentAnimatorStateInfo(1);
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        currentLArmState = anim.GetCurrentAnimatorStateInfo(3);
        currentPickUpState = anim.GetCurrentAnimatorStateInfo(4);
        currentAssKickState = anim.GetCurrentAnimatorStateInfo(2);
    }

    void Update ()
    {
        BasicMovement();
        if (hasWeapon)
        {
            MoveWhileEquip();
        }
        WeaponSelect();
        Larm();
        GetAssKicked();

        if (anim.GetBool("GetHitHard"))
        {
            GetHitHard();
        }

        if (hasWeapon)
        {
            ChargeAttack();
        }

    }
}
