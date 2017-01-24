﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour
{
    private GameObject player;
    private Unit unitClass;
    private MinionStats stats;
    public float distance, timeBetweenAttacks, myTargetChance, thinkingSpd;
    private float decideTarget;
    private bool reCheck, attack;
    [HideInInspector]
    public GameObject controllObject;
    private Transform myTarget;
    private Coroutine curCoroutine;
    private PBAnim pBAnim;
    private ControlPoint controlPoint;
    private Coroutine attackCoroutine;
    private Unit myUnit;

    void Start()
    {
        myUnit = GetComponent<Unit>();
        pBAnim = GetComponent<PBAnim>();
        player = GameObject.FindGameObjectWithTag("Champion");
        controllObject = GameObject.FindGameObjectWithTag("ControllPoint");
        controlPoint = controllObject.GetComponent<ControlPoint>();
        unitClass = GetComponent<Unit>();
        DecideTarget();
        stats = GetComponent<MinionStats>();
        curCoroutine = StartCoroutine(ThinkAboutNextAction());
    }

    public void EndLife()
    {
        StopCoroutine(curCoroutine);
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

    void CheckDistance()
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

    }

    IEnumerator Attack()
    {
        pBAnim.Attack();
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackCoroutine = (StartCoroutine(Attack()));
    }
}
