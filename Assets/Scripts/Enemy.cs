using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] private GameObject impactParticle;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeHit(Character hitBy)
    {
        animator.SetTrigger("Die");

        Instantiate(impactParticle, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        
        Destroy(gameObject, 6);
    }
}