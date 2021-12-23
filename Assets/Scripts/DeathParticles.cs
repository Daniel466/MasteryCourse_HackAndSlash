using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private PooledMonoBehaviour deathParticlePrefab;
    
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        character.OnDied += Character_OnDied;
    }

    private void Character_OnDied(Character character)
    {
        character.OnDied -= Character_OnDied;
        deathParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        character.OnDied -= Character_OnDied;
    }
}