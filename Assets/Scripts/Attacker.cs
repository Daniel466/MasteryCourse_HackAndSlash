using System;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField] private int damage = 1;
    
    [SerializeField] private float attackRefreshSpeed = 1.5f;
    
    private float attackTimer;

    public int Damage { get { return damage; } }
    
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        target.TakeHit(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
}