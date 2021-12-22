using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float respawnRate = 10;
    [SerializeField] private float initialSpawnDelay;
    [SerializeField] private int totalNumberToSpawn;
    [SerializeField] private int numberToSpawnEachTime = 1;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (ShouldSpawn())
            Spawn();
    }
    
    private bool ShouldSpawn()
    {
        return spawnTimer >= respawnRate;
    }

    private void Spawn()
    {
        spawnTimer = 0;

        Enemy prefab = ChooseRandomEnemyPrefab();
        if (prefab != null)
        {
            Transform spawnPoint = ChooseRandomSpawnPoint();
            var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private Transform ChooseRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
            return transform;

        if (spawnPoints.Length == 1)
            return spawnPoints[0];
        
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);

        return spawnPoints[index];
    }

    private Enemy ChooseRandomEnemyPrefab()
    {
        if (enemyPrefabs.Length == 0)
            return null;

        if (enemyPrefabs.Length == 1)
            return enemyPrefabs[0];
        
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        return enemyPrefabs[index];
    }
}