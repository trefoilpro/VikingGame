using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Camera _camera;
    
    public void SpawnEnemy(int numberOfEnemy, int enemyHealth, int enemyDamage, Vector3 spawnCenter, float spawnRadius)
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            
            
            Vector3 spawnPosition = RandomPointGenerator.GetRandomPointOnNavMesh(spawnRadius, spawnCenter);
            Enemy enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.Initialize(enemyHealth, enemyDamage, this, _camera);
        }
    }
}
