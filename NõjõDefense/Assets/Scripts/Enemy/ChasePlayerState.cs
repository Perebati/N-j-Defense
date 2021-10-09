using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasePlayerState : BaseState
{

    private float chaseSpeed = 1f;
    private float timer = 0f;
    GameObject player => GameObject.FindWithTag("Player");

    private Enemy enemy;
    Seeker seeker;
    Rigidbody rb;

    Path _path;
    private float nextWaypointDistance = .7f; // !
    int currentWaypoint = 0;
    bool reachedTarget = false;

    public ChasePlayerState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        seeker = this.enemy.GetComponent<Seeker>();
        rb = this.enemy.GetComponent<Rigidbody>();
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

        if (_path == null)
        {
            return typeof(ChasePlayerState);
        }

        Chase();
        return CheckTarget();
    }

    private Type CheckTarget() // check if the player is in range, else, target = monument
    {
        if (Vector3.Distance(rb.position, player.transform.position) > 11f)
        {        
            return typeof(WanderState);
        }
        else if (currentWaypoint >= _path.vectorPath.Count)
        {
            reachedTarget = true;
            //return typeof(AttackState);
            return typeof(ChasePlayerState);
        }
        else
        {
            reachedTarget = false;
            return typeof(ChasePlayerState);
        }
    }

    private void Chase()
    {

        Vector3 dir = (_path.vectorPath[currentWaypoint] - rb.position).normalized;
        transform.position += dir * chaseSpeed * Time.deltaTime;
        float distance = Vector3.Distance(rb.position, _path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance && _path.vectorPath.Count > currentWaypoint)
            currentWaypoint++;
    }

}
