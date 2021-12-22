using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static List<Character> All = new List<Character>();
    
    private Controller controller;
    private Animator animator;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackOffset = 1f;
    
    [SerializeField] private float attackRadius = 1f;
    
    private Collider[] attackResults;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackResults = new Collider[10];

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        animationImpactWatcher.OnImpact += AnimationImpactWatcherOnOnImpact;
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
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = direction * 360f;
            
            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (controller.attackPressed)
        {
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcherOnOnImpact()
    {
        Vector3 position = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
                takeHit.TakeHit(this);
        }
    }

    private void OnEnable()
    {
        if (All.Contains(this) == false)
            All.Add(this);
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }
}