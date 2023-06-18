using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private Score _score;
    [SerializeField] private GameOverMenu _gameOverMenu;

    public Score GetScore() => _score;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameStarter.StartGame();
    }

    public void GameOver()
    {
        _gameOverMenu.ShowGameOverMenu();
    }
}
