using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float launchYOffset = 1f;
    
    private Animator animator;
    
    public int Damage { get { return 1; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        projectilePrefab.Get<Projectile>(transform.position + Vector3.up * launchYOffset, transform.rotation);
    }

    protected override void OnTryUse()
    {
        Attack();
    }
}