using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        MISERAVEL, ATIRADOR, ESCUDO, PERSEGUIDOR, SAMURAI
    }

    public EnemyType enemyType;
    public float speed = 5f;
    public float playerDetectionRadius = 10f;
    public float attackCooldown = 1f;
    public GameObject projectilePrefab;

    [SerializeField] private int maxHealth = 100;
    public int attackRange = 1;

    private int currentHealth;
    public Transform _target { get; private set; } // player or monument
    public StateMachine StateMachine => GetComponent <StateMachine>();

    private void Awake()
    {
        _target = null;
        InitializeStateMachine();
        currentHealth = maxHealth;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this) },
            { typeof(ChasePlayerState), new ChasePlayerState(this) },
            { typeof(AttackState), new AttackState(this) }
        };

        StateMachine.SetStates(states);
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Died");
    }

}
