using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Character target;

    private bool IsDead { get { return currentHealth <= 0; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
            target = Character.All
                .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                .FirstOrDefault();
            
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target. transform.position);

            if (distance > 2)
            {
                animator.SetFloat("Speed", 1f);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.transform.position);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
                navMeshAgent.isStopped = true;
                // Attack
            }
        }
    }

    public void TakeHit(Character hitBy)
    {
        currentHealth--;
        
        Instantiate(impactParticle, transform.position + new Vector3(0, 2, 0), Quaternion.identity);

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

        Destroy(gameObject, 6);
    }
}