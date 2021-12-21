using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeHit(Character hitBy)
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 6);
    }
}