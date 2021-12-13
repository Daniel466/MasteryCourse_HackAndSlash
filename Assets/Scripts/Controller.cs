using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index;// { get; private set; }

    private string attackButton;

    public bool attack;
    
    private void Start()
    {
        //Index = 1;
        attackButton = "Attack" + Index;
    }

    private void Update()
    {
        attack = Input.GetButton(attackButton);
    }
}