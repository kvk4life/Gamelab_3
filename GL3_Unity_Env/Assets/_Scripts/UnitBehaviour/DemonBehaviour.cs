using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonBehaviour : MonoBehaviour
{

    private GameObject player;
    private Unit unitClass;
    private DemonStats stats;
    public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd, hp;
    private float decideTarget;
    private bool reCheck, attack;
    [HideInInspector]
    public GameObject controllObject;
    private Transform myTarget;
    private bool barbellTargeted;
    private Coroutine curCoroutine;
    private Animator anim;
    private ControlPoint controlPoint;
    private Coroutine attackCoroutine;
    private Unit myUnit;


    void Start()
    {
        myUnit = GetComponent<Unit>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        controlPoint = controllObject.GetComponent<ControlPoint>();
        unitClass = GetComponent<Unit>();
        DecideTarget();
        stats = GetComponent<DemonStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    public void EndLife()
    {
        GetComponent<DemonRag>().RagActive();
        StopCoroutine(curCoroutine);
    }

    IEnumerator ThinkAboutNextAction()
    {
        CheckDistance();
        yield return new WaitForSeconds(thinkingSpd);
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    public void BarbellTarget(GameObject barbell)
    {
        myTarget = barbell.transform;
        barbellTargeted = true;
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

    void CheckDistance()
    { // checks for the distance beteen the player and the enemy
        DecideTarget();
        float dist = Vector3.Distance(myTarget.position, transform.position);
        if (dist <= distance)
        {
            if (barbellTargeted)
            {

            }
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
        int rollAttack = Random.Range(0, 2);
        if (rollAttack < 1)
        {
            anim.SetTrigger("Attack1");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackCoroutine = (StartCoroutine(Attack()));
    }
}
