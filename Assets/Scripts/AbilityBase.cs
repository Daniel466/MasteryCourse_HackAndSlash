using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] private float attackRefreshSpeed = 1.5f;

    [SerializeField] private PlayerButton button;
   
    protected float attackTimer;
    
    private Controller controller;

    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    public void SetController(Controller controller)
    {
        this.controller = controller;
        foreach (var ability in GetComponents<AbilityBase>())
        {
            ability.SetController(controller);
        }
    }
    
    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (controller != null &&
            CanAttack &&
            controller.ButtonDown(button))
        {
            OnTryUse();
        }
    }

    protected abstract void OnTryUse();
}