using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasePlayerState : BaseState
{
    private float timer = 0f;
    GameObject player;

    private Enemy enemy;
    Seeker seeker;
    Rigidbody rb;

    Path _path;
    private float nextWaypointDistance = 1f;
    int currentWaypoint = 0;

    public ChasePlayerState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        seeker = this.enemy.GetComponent<Seeker>();
        rb = this.enemy.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        if (enemy.enemyType != Enemy.EnemyType.MISERAVEL)
            enemy.SetTarget(player.transform);
    }
    
    private void OnPathComplete(Path path) // Callback
    {
        if(!path.error)
        {
            _path = path;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        timer = 0;
        if (seeker.IsDone())
            seeker.StartPath(enemy.enemyFeet.position, enemy._target.position, OnPathComplete);
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;
        enemy.SetTarget(player.transform);
        if (timer > 1f)
            UpdatePath();

        if (_path == null || enemy._target != player.transform)
        {
           
            return typeof(WanderState);
        }
        Chase();
        return CheckTarget();
    }

    private Type CheckTarget()
    {
        if (enemy.enemyType == Enemy.EnemyType.MISERAVEL && Vector3.Distance(enemy.transform.position, player.transform.position) > enemy.playerDetectionRadius)
        {
            _path = null;
            GFX gfx = transform.GetComponentInChildren<GFX>();  
            gfx.anim.SetTrigger("Turn1");
            return typeof(WanderState);
        }
        else if (Vector3.Distance(enemy.transform.position, player.transform.position) <= enemy.attackRange + 2f)
        {    
            _path = null;
            return typeof(AttackState);
        }
        else
        {
            return typeof(ChasePlayerState);
        }
    }

    private void Chase()
    {     
        if (_path.vectorPath.Count > currentWaypoint)
        {
            Vector3 dir = (_path.vectorPath[currentWaypoint] - enemy.enemyFeet.position).normalized;
            //rb.velocity = dir * enemy.speed * Time.deltaTime;
            transform.position += dir * enemy.speed * Time.deltaTime;
            float distance = Vector3.Distance(enemy.enemyFeet.position, _path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance && _path.vectorPath.Count > currentWaypoint)
                currentWaypoint++;
        }
    } 
}
