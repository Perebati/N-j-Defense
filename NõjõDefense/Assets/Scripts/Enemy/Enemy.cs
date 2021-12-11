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
    [SerializeField] public float attackCooldown = 1f;
    public float enemyDamage = 10f;
    public GameObject projectilePrefab;
    public Transform enemyFeet;

    public int attackRange = 1;

    public Transform _target { get; private set; } // player or monument
    public StateMachine StateMachine => GetComponent <StateMachine>();

    private void Awake()
    {
        _target = null;
        InitializeStateMachine();
    }

    private void Start()
    {
        enemyFeet = transform.GetChild(1);
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

    

    float timer = 0;

}
