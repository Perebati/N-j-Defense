using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class WanderState : BaseState
{
    private Enemy enemy; 
    GameObject _player => GameObject.FindWithTag("Player");
    GameObject[] monument;

    Seeker seeker;
    Rigidbody rb;
    Path _path;
    private float nextWaypointDistance = 2f; // !
    int currentWaypoint = 0;
    private float timer = 0f;

    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        monument = GameObject.FindGameObjectsWithTag("Monument");
        rb = this.enemy.GetComponent<Rigidbody>();
        seeker = this.enemy.GetComponent<Seeker>();
    }

    public override Type Tick()
    {
        if (enemy.enemyType != Enemy.EnemyType.MISERAVEL)
            return typeof(ChasePlayerState);

        var player = SearchTarget();

        if (player != null)
        {        
            return typeof(ChasePlayerState);
        }

        if (enemy._target != player || enemy._target == null)
            enemy.SetTarget(NearestMonument());

        timer += Time.deltaTime;
        if (timer > 1f)
            UpdatePath();

        if (_path == null)
        {
            return typeof(WanderState);
        }

        float targetDistance = Chase();

        if (targetDistance < enemy.attackRange)
        {
            _path = null;
            return typeof(AttackState);
        }
        else 
            return typeof(WanderState);
    }

    private Transform SearchTarget() {

        if (Vector3.Distance(_player.transform.position, enemy.transform.position) < enemy.playerDetectionRadius)
        {
            enemy.SetTarget(_player.transform);
            //GFX gfx = transform.GetComponentInChildren<GFX>();
            //gfx.anim.SetTrigger("Turn");
            return _player.transform;
        }     
        else
            return null;
    }

    private Transform NearestMonument()
    {
        int index = 1;

        float d0 = Vector3.Distance(enemy.transform.position, monument[0].transform.position);
        float d1 = Vector3.Distance(enemy.transform.position, monument[1].transform.position);
        float d2 = Vector3.Distance(enemy.transform.position, monument[2].transform.position);

        if (monument[0].activeSelf && d0 < d1 && d0 < d2)      
            index = 0;   
        else 
        if (monument[2].activeSelf && d2 < d0 && d2 < d1)
            index = 2;

        return monument[index].transform;
    }
    private void OnPathComplete(Path path) // Callback
    {
        if (!path.error)
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

    private float Chase()
    {
        float targetDist = Vector3.Distance(enemy.transform.position, enemy._target.position);

        if (_path.vectorPath.Count > currentWaypoint)
        {
            Vector3 dir = (_path.vectorPath[currentWaypoint] - rb.position).normalized;
            transform.position += dir * enemy.speed * Time.deltaTime;
            float distance = Vector3.Distance(rb.position, _path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance && _path.vectorPath.Count > currentWaypoint)
                currentWaypoint++;
        }

        return targetDist;

    }

}
