using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderState : BaseState
{
    private Enemy enemy; //this go
    GameObject player => GameObject.Find("Player");
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        this.gameObject.transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
        var player = SearchTarget();

        if (player != null)
        {
            enemy.SetTarget(player);
            return typeof(ChasePlayerState);
        }

        return null; // typeof(WanderState);
    }

    private Transform SearchTarget() {

        if (Vector3.Distance(player.transform.position, enemy.transform.position) < 10f)
            return player.transform;

        return null;
    }
}
