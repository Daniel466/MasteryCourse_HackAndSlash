using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] private float forceAmount = 10f;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeHit(IAttack hitBy)
    {
        var direction = transform.position - hitBy.transform.position;
        direction.Normalize();
        
        rigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}