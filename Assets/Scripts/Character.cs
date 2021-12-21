using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller controller;
    
    [SerializeField] private float moveSpeed = 5f;

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection();
        if (direction.magnitude > 0.25f)
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
    }
}