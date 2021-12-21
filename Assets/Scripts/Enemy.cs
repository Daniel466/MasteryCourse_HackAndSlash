using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    public void TakeHit(Character hitBy)
    {
        Destroy(gameObject);
    }
}