using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    private void Start()
    {
        Player player = Instantiate(_playerPrefab, new Vector3(212.8f, 20.9f, -25.3f), Quaternion.identity);
        player.Initialize(20, 1);

        _cinemachineVirtualCamera.LookAt = player.GetFollowTarget().transform;
        _cinemachineVirtualCamera.Follow = player.GetFollowTarget().transform;
        
        Enemy enemy = Instantiate(_enemyPrefab, new Vector3(223.36f, 18.84f, -29.82f), Quaternion.identity);
        enemy.Initialize(1, 1);

    }
}
