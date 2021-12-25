using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 10;
    
    private Controller controller;
    private IAttack attacker;
    private Animator animator;
    private int currentHealth;
    private new Rigidbody rigidbody;

    public event Action<int, int> OnHealthChanged = delegate {  };
    public event Action<IDie> OnDied = delegate { };
    
    public int Damage { get { return damage; } }

    private void Awake()
    {
        attacker = GetComponent<IAttack>();
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection();
        if (direction.magnitude > 0.25f)
        {
            var velocity = (direction * moveSpeed).With(y: rigidbody.velocity.y);
            rigidbody.velocity = velocity;
            transform.forward = direction * 360f;
            
            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (controller.attackPressed)
        {
            if (attacker.CanAttack)
            {
                attacker.Attack();
            }
        }
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        
        if (All.Contains(this) == false)
            All.Add(this);
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }

    public void TakeHit(IAttack hitBy)
    {
        if (currentHealth <= 0)
            return;
        
        currentHealth -= hitBy.Damage;
        OnHealthChanged(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied(this);
    }
}