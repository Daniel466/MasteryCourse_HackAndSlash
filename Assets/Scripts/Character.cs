using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller controller;
    private Animator animator;
    
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
    }
}