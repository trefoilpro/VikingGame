using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private  EnemyAnimationsHandler _enemyAnimationsHandler;
    [SerializeField] private Collider _collider;

    private EnemySpawner _enemySpawner;
    public bool CanMove { get; private set; }
    
    public int CurrentHealth { get; private set; }
    
    public int MaxHealth { get; set; }
    public int Damage { get; set; }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CanMove = false;
            _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Die);
            _collider.enabled = false;
            GameManager.Instance.GetScore().AddScore(1);
            _enemySpawner.SpawnEnemy(1, MaxHealth + 1, 1, Player.Instance.transform.position, 200f);
        }
    }

    public void SetCanMove(bool variable) => CanMove = variable;

    private void Awake()
    {
        CanMove = true;
    }

    public void Initialize(int health, int damage, EnemySpawner enemySpawner)
    {
        MaxHealth = health;
        CurrentHealth = health;
        Damage = damage;
        _enemySpawner = enemySpawner;
    }
}
