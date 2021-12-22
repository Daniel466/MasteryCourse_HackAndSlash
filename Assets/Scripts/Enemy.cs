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
    private Character target;
    private NavMeshAgent navMeshAgent;

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
        if (target == null)
        {
            target = Character.All
                .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                .FirstOrDefault();
        }
        else
        {
            navMeshAgent.SetDestination(target.transform.position);
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
        
        Destroy(gameObject, 6);
    }
}