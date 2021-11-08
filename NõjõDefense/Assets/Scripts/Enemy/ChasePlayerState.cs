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
    private float nextWaypointDistance = .8f;
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
            seeker.StartPath(enemy.transform.position, enemy._target.position, OnPathComplete);
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;
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
            return typeof(WanderState);
        }
        else if (Vector3.Distance(enemy.transform.position, player.transform.position) <= enemy.attackRange)
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
            Vector3 dir = (_path.vectorPath[currentWaypoint] - rb.position).normalized;
            transform.position += dir * enemy.speed * Time.deltaTime;
            float distance = Vector3.Distance(rb.position, _path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance && _path.vectorPath.Count > currentWaypoint)
                currentWaypoint++;
        }
    } 
}
