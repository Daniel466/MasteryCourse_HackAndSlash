using UnityEngine;

public class Projectile : PooledMonoBehaviour, IDamage
{
    [SerializeField] private float moveSpeed = 10f;
    
    public int Damage { get { return 1; } }
    
    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.collider.GetComponent<ITakeHit>();
        if (hit != null)
        {
            Impact(hit);
        }
        else
        {
            ReturnToPool();
        }
    }

    private void Impact(ITakeHit hit)
    {
        hit.TakeHit(this);
    }
}