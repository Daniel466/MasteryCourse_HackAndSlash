using UnityEngine;

public class Jump : AbilityBase
{
    [SerializeField] private float jumpForce = 100f;
    
    private new Rigidbody rigidbody;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnUse()
    {
        rigidbody.AddForce(Vector3.up * jumpForce);
    }
}