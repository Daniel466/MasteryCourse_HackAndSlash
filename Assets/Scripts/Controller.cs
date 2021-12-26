using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    
    private string attackButton;
    private string specialAttackButton;
    private string jumpButton;
    
    private string horizontalAxis;
    private string verticalAxis;

    public bool attack;
    public bool attackPressed;
    public bool specialAttackPressed;
    public bool jumpPressed;
    public float horizontal;
    public float vertical;

    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
        {
            attack = Input.GetButton(attackButton);
            attackPressed = Input.GetButtonDown(attackButton);
            specialAttackPressed = Input.GetButtonDown(specialAttackButton);
            jumpPressed = Input.GetButtonDown(jumpButton);
            
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        specialAttackButton = "SpecialAttack" + Index;
        jumpButton = "Jump" + Index;
        
        horizontalAxis = "Horizontal" + index;
        verticalAxis = "Vertical" + index;
        gameObject.name = "Controller" + Index;
    }
    
    internal bool ButtonDown(PlayerButton button)
    {
        switch (button)
        {
            case PlayerButton.Attack:
                return attackPressed;
            case PlayerButton.SpecialAttack:
                return specialAttackPressed;
            case PlayerButton.Jump:
                return jumpPressed;
            default:
                return false;
        }
    }

    public bool AnyButtonDown()
    {
        return attack;
    }
    
    internal Vector3 GetDirection()
    {
        return new Vector3(horizontal, 0, -vertical);
    }
}