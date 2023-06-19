using Cinemachine;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private int _numberOfEnemy;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private GameObject _mapCenter;
    [SerializeField] private HealthBar healthBar;
    
    public void StartGame()
    {
        Player player = Instantiate(_playerPrefab, _mapCenter.transform.position, Quaternion.identity);
        player.Initialize(20, 1, healthBar);

        _cinemachineVirtualCamera.LookAt = player.GetFollowTarget().transform;
        _cinemachineVirtualCamera.Follow = player.GetFollowTarget().transform;

        _enemySpawner.SpawnEnemy(_numberOfEnemy, 1, 1, player.transform.position, 100f);
    }
}
