using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private PooledMonoBehaviour deathParticlePrefab;
    
    private IDie entity;

    private void Awake()
    {
        entity = GetComponent<IDie>();
    }

    private void OnEnable()
    {
        entity.OnDied += EntityOnDied;
    }

    private void EntityOnDied(IDie entity)
    {
        entity.OnDied -= EntityOnDied;
        deathParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        entity.OnDied -= EntityOnDied;
    }
}