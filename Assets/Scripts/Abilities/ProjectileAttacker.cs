using System.Collections;
using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float launchYOffset = 1f;
    [SerializeField] private float launchDelay = 1f;
    
    private Animator animator;
    
    public int Damage { get { return 1; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        StartCoroutine(LaunchAfterDelay());
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(launchDelay);
        projectilePrefab.Get<Projectile>(transform.position + Vector3.up * launchYOffset, transform.rotation);
    }

    protected override void OnUse()
    {
        Attack();
    }
}