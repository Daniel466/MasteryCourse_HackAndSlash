using System;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] private int initialPoolSize = 50;

    public event Action<PooledMonoBehaviour> OnReturnToPool;

    public int InitialPoolSize { get { return initialPoolSize; } }

    private void OnDisable()
    {
        if (OnReturnToPool != null)
            OnReturnToPool(this);
    }
}