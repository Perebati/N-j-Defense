using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : BaseState
{
    private Enemy enemy;
    private Rigidbody rb;
    GameObject player;

    private float timer = 1f;

    public AttackState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        rb = this.enemy.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;
        if (timer >= enemy.attackCooldown)
            Attack();

        var nextState = NextState();
        return nextState;
    }

    public void Attack()
    {
        timer = 0;

        Debug.Log(enemy.enemyType + " atacou");
        //animacao de atk
    }
    private Type NextState()
    {

        switch (enemy.enemyType)
        {
            case Enemy.EnemyType.MISERAVEL:

                if (Vector3.Distance(player.transform.position, enemy.transform.position) < enemy.playerDetectionRadius)
                {
                    return typeof(ChasePlayerState);
                }
                break;

            default:
                if (Vector3.Distance(player.transform.position, enemy.transform.position) > enemy.attackRange + 3f)
                {
                    return typeof(ChasePlayerState);
                }
                break;
        }
        return typeof(AttackState);
    }
}
