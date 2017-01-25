using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : DemonBehaviour
{
    //private GameObject player;
    //private Unit unitClass;
    private MinionStats stat;
    //public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd;
    //private float decideTarget;
    //private bool reCheck, attack;
    [HideInInspector]
    //public GameObject controllObject;
    //private Transform myTarget;
    //private Coroutine curCoroutine;
    //private PBAnim pBAnim;
    //private ControlPoint controlPoint;
    //private Coroutine attackCoroutine;
    //private Unit myUnit;

    public new void Start()
    {
        base.Start();
        myUnit = GetComponent<Unit>();
        if (GetComponent<PBAnim>() != null) {
            pBAnim = GetComponent<PBAnim>();
        }
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        controlPoint = controllObject.GetComponent<ControlPoint>();
        unitClass = GetComponent<Unit>();
        DecideTarget();
        stat = GetComponent<MinionStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    public new void EndLife()
    {
        StopCoroutine(curCoroutine);
        player.GetComponent<GoldMng>().AddGold(gold);
        GetComponent<DemonRag>().RagActive();
        //StopCoroutine(curCoroutine);
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
        }
        else
        {
            myTarget = player.transform;
        }
        myUnit.target = myTarget;
    }

    /*void CheckDistance()
    {
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

    }*/

    IEnumerator Attack()
    {
        if (pBAnim != null) {
            pBAnim.Attack();
        }
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackCoroutine = (StartCoroutine(Attack()));
    }
}
