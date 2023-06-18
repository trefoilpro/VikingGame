using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private RandomPointGenerator _randomPointGenerator;
    public void SpawnEnemy(int numberOfEnemy, int enemyHealth, int enemyDamage, Vector3 spawnCenter)
    {
        Enemy enemy = Instantiate(_enemyPrefab, new Vector3(223.36f, 18.84f, -29.82f), Quaternion.identity);
        enemy.Initialize(1, 1);
    }
}
