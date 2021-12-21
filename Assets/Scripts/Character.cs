using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller controller;

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }
}