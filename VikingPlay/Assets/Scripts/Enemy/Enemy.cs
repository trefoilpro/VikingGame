using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private  EnemyAnimationsHandler _enemyAnimationsHandler;
    [SerializeField] private Collider _collider;
    [SerializeField] private Billboard _billboard;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthSphere _healthSpherePrefab;

    
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
            _healthBar.SetHealth(CurrentHealth);
            return;
        }
        
        _healthBar.SetHealth(CurrentHealth);
    }

    public void SetCanMove(bool variable) => CanMove = variable;
    public HealthBar GetHealthBar() => _healthBar;

    private void Awake()
    {
        CanMove = true;
    }

    public void Initialize(int health, int damage, EnemySpawner enemySpawner, Camera camera)
    {
        MaxHealth = health;
        CurrentHealth = health;
        Damage = damage;
        _enemySpawner = enemySpawner;
        _billboard.Initialize(camera);
        _healthBar.SetMaxHealth(MaxHealth);
        _healthBar.SetHealth(CurrentHealth);
    }

    public void SpawnHealthSphere()
    {
        if (Random.Range(0, 100) < 30)
        {
            Instantiate(_healthSpherePrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }
}
