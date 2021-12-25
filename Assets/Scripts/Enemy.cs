﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{
    [SerializeField] private PooledMonoBehaviour impactParticle;
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;
    private Character target;

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<IDie> OnDied = delegate { };

    private bool IsDead { get { return currentHealth <= 0; } }
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (IsDead)
            return;
        
        if (target == null)
        {
            AquireTarget();
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target. transform.position);

            if (distance > 2)
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    private void AquireTarget()
    {
        target = Character.All
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();

        animator.SetFloat("Speed", 0f);
    }

    private void FollowTarget()
    {
        animator.SetFloat("Speed", 1f);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void TryAttack()
    {
        animator.SetFloat("Speed", 0f);
        navMeshAgent.isStopped = true;

        if (attacker.CanAttack)
        {
            attacker.Attack(target);
        }
    }

    public void TakeHit(IDamage hitBy)
    {
        currentHealth -= hitBy.Damage;

        OnHealthChanged(currentHealth, maxHealth);
        
        impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;

        OnDied(this);

        ReturnToPool(6f);
    }
}