using System.Collections;
 using UnityEngine;
 
 public class Attacker : AbilityBase, IAttack
 {
     [SerializeField] private int damage = 1;
     [SerializeField] private float attackOffset = 1f;
     [SerializeField] private float attackRadius = 1f;
     [SerializeField] private float attackImpactDelay = 1f;
     [SerializeField] private float attackRange = 2f;
     
     private Collider[] attackResults;
     private Animator animator;
 
     public int Damage { get { return damage; } }
     
     private void Awake()
     {
         animator = GetComponentInChildren<Animator>();
         
         var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
         if (animationImpactWatcher != null)
         {
             animationImpactWatcher.OnImpact += AnimationImpactWatcherOnOnImpact;
         }
         
         attackResults = new Collider[10];
     }
 
     public void Attack(ITakeHit target)
     {
         attackTimer = 0;
         StartCoroutine(DoAttack(target));
     }
 
     private IEnumerator DoAttack(ITakeHit target)
     {
         yield return new WaitForSeconds(attackImpactDelay);
 
         if (target.Alive && InAttackRange(target))
         {
             target.TakeHit(this);
         }
     }
     
     public bool InAttackRange(ITakeHit target)
     {
         if (target.Alive == false)
             return false;
         
         var distance = Vector3.Distance(transform.position, target.transform.position);
         return distance < attackRange;
     }
 
     /// <summary>
     /// Called by animation event via AnimationImpactWatcher
     /// </summary>
     private void AnimationImpactWatcherOnOnImpact()
     {
         Vector3 position = transform.position + transform.forward * attackOffset;
         int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults);
 
         for (int i = 0; i < hitCount; i++)
         {
             var takeHit = attackResults[i].GetComponent<ITakeHit>();
             if (takeHit != null)
                 takeHit.TakeHit(this);
         }
     }
     
     public void Attack()
     {
         animator.SetTrigger("Attack");
     }
 
     protected override void OnUse()
     {
         Attack();
     }
 }