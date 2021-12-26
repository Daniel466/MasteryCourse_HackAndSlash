using System.Collections;
using UnityEngine;

public class Knockback : AbilityBase, IDamage
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float impactDelay = 0.25f;
    [SerializeField] private float forceAmount = 10f;

    private Collider[] attackResults;

    public int Damage { get { return damage; } }

    private void Awake()
    {
        attackResults = new Collider[10];
    }

    private void Attack()
    {
        StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(impactDelay);
        
        Vector3 position = transform.position + transform.forward;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults);
 
        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
            {
                takeHit.TakeHit(this);
            }

            var hitRigidbody = attackResults[i].GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                var direction = hitRigidbody.transform.position - transform.position;
                direction.Normalize();
        
                hitRigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
            }
        }
    }
    
    protected override void OnUse()
    {
        Attack();
    }
}