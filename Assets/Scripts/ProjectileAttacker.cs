﻿using UnityEngine;

public class ProjectileAttacker : MonoBehaviour, IAttack
{
    [SerializeField] private float attackRefreshSpeed = 1f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float launchYOffset = 1f;
    
    private float attackTimer;
    private Animator animator;
    
    public int Damage { get { return 1; } }
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        projectilePrefab.Get<Projectile>(transform.position + Vector3.up * launchYOffset, transform.rotation);
    }
}