using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] private float attackRefreshSpeed = 1.5f;
    [SerializeField] private PlayerButton button;
    [SerializeField] protected string animationTrigger;
   
    protected float attackTimer;
    
    private Controller controller;
    private Animator animator;

    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }
    
    protected abstract void OnUse();
    
    public void SetController(Controller controller)
    {
        this.controller = controller;
    }
    
    private void Update()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        
        attackTimer += Time.deltaTime;

        if (ShouldTryUse())
        {
            if (string.IsNullOrEmpty(animationTrigger))
            {
                animator.SetTrigger(animationTrigger);
            }
            
            OnUse();
        }
    }

    private bool ShouldTryUse()
    {
        return controller != null && CanAttack && controller.ButtonDown(button);
    }
}