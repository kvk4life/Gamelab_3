using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonBehaviour : MonoBehaviour
{
    public GameObject player;
    protected Unit unitClass;
    protected DemonStats stats;
    public int damage;
    public int gold;
    public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd;
    protected float decideTarget;
    protected bool reCheck, attack;
    [HideInInspector]
    public GameObject controllObject;
    [HideInInspector]
    public Transform myTarget;
    [HideInInspector]
    public GameObject curTar;
    protected Coroutine curCoroutine;
    protected Animator anim;
    protected ControlPoint controlPoint;
    protected Coroutine attackCoroutine;
    protected Unit myUnit;
    protected PBAnim pBAnim;


    public void Start()
    {
        myUnit = GetComponent<Unit>();
        if (GetComponent<PBAnim>() != null) {
            pBAnim = GetComponent<PBAnim>();
        }
        else {
            anim = GetComponent<Animator>();
        }
        player = GameObject.FindWithTag("Champion");
        myUnit.target = player.transform;
        controllObject = GameObject.FindWithTag("ControllPoint");
        controlPoint = controllObject.GetComponent<ControlPoint>();
        unitClass = GetComponent<Unit>();
        DecideTarget();
        stats = GetComponent<DemonStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    public void EndLife()
    {
        player.GetComponent<GoldMng>().AddGold(gold);
        GetComponent<DemonRag>().RagActive();
        StopCoroutine(curCoroutine);
        GetComponent<DemonRoundSystem>().wave.EnemyKilled();
    }

    IEnumerator ThinkAboutNextAction()
    {
        CheckDistance();
        yield return new WaitForSeconds(thinkingSpd);
        DecideTarget();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    void DecideTarget()
    {
        if (controlPoint.myCrystal.activeSelf && player.activeSelf)
        {
            if (decideTarget == 0)
            {
                decideTarget = Random.Range(1, 101);
            }
            myTarget = (decideTarget < myTargetChance) ? player.transform : controllObject.transform;
            curTar = (decideTarget < myTargetChance) ? player : controllObject;
        }
        else
        {
            myTarget = player.transform;
            curTar = player;
        }
        print("target = " + myTarget);
    }

    public void CheckDistance()
    { // checks for the distance beteen the player and the enemy
        float dist = Vector3.Distance(myTarget.position, transform.position);
        if (dist <= distance)
        {
            if (!attack)
            {
                attackCoroutine = (StartCoroutine(Attack()));
                reCheck = true;
                myUnit.StopPathCoroutine();
                attack = true;
            }
        }
        else
        {
            attack = false; ;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            };
            if (reCheck)
            {
                if (controlPoint.myCrystal.activeSelf)
                {
                    unitClass.target = controllObject.transform;
                }
                else
                {
                    unitClass.target = player.transform;
                }
                myUnit.StartPathCoroutine();
                reCheck = false;
            }
        }

    }

    IEnumerator Attack()
    {
        //speel de attack animatie af
        if (pBAnim != null) {
            pBAnim.Attack();
        }
        else {
            int rollAttack = Random.Range(0, 2);
            if (rollAttack < 1)
            {
                anim.SetTrigger("Attack1");
            }
            else
            {
                anim.SetTrigger("Attack2");
            }
        }
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackCoroutine = (StartCoroutine(Attack()));
    }
}
