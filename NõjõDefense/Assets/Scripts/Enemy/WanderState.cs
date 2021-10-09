using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderState : BaseState
{
    private Enemy enemy; 
    GameObject player => GameObject.FindWithTag("Player");
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
        var player = SearchTarget();

        if (player != null)
        {
           
            return typeof(ChasePlayerState);
        }

        return typeof(WanderState);
    }

    private Transform SearchTarget() {

        if (Vector3.Distance(player.transform.position, enemy.transform.position) < 10f)
        {
            enemy.SetTarget(player.transform);
            return player.transform;
        }

        return null;
    }
}
