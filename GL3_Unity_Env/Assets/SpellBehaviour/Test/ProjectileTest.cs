﻿using UnityEngine;
using System.Collections;

public class ProjectileTest : Spell
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Projectile newProj = new Projectile();
            print("shot");
            newProj.FireProjectile(transform,projectile,damage,travelSpeed,duration,initialDamage,dotTotalDamage,dotTickSpeed,spellEffect);
        }
    }
}
