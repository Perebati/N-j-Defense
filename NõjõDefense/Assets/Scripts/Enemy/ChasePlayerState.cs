using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : BaseState
{
    private Enemy enemy;
    private Transform target;
    public ChasePlayerState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        Debug.Log("Chasing Player");

        return typeof(ChasePlayerState);

       // return null;
    }
}
