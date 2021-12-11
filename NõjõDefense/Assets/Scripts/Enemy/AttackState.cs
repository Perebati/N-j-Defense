using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : BaseState
{
    private Enemy enemy;
    private Rigidbody rb;
    GameObject player;

    private float timer;

    private bool canAttack = true;

    public AttackState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        rb = this.enemy.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        timer = 9f; // !-------------------------------------
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
        GFX gfx = transform.GetComponentInChildren<GFX>();
        gfx.Attack();
        if (Vector3.Distance(player.transform.position, enemy.enemyFeet.position) < 10f) {
            AttackAudio atkAudio = transform.GetComponent<AttackAudio>();
            atkAudio.PlayAttackAudio();
        }
        if (enemy.enemyType != Enemy.EnemyType.ATIRADOR)
        {
            enemy._target.GetComponent<Health>().TakeDamage(enemy.enemyDamage);
            return;
        }
               
        GameObject projectile = GameObject.Instantiate(enemy.projectilePrefab, gfx.firePoint.position, gfx.transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(gfx.firePoint.forward * projectile.GetComponent<Projectile>().bulletSpeed, ForceMode.Impulse);
        projectile.GetComponent<Projectile>().Start();          
        

    }
    private Type NextState()
    {
        GFX gfx = transform.GetComponentInChildren<GFX>();

        switch (enemy.enemyType)
        {
            case Enemy.EnemyType.MISERAVEL:

                if (Vector3.Distance(player.transform.position, enemy.enemyFeet.position) < Vector3.Distance(enemy._target.transform.position, enemy.enemyFeet.position) + enemy.attackRange + 3 && !enemy._target.CompareTag("Player"))
                {
                    gfx.anim.SetBool("Attack", false);
                    return typeof(ChasePlayerState);
                }

                if (!enemy._target.transform.gameObject.activeSelf )
                {
                    gfx.anim.SetBool("Attack", false);
                    return typeof(ChasePlayerState);
                }
                break;

            default:
                if (Vector3.Distance(player.transform.position, enemy.transform.position) > enemy.attackRange + 3f)
                {
                    gfx.anim.SetBool("Attack", false);
                    return typeof(ChasePlayerState);
                }
                break;
        }
        return typeof(AttackState);
    }

}
