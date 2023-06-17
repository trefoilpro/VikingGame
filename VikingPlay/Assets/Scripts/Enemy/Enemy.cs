using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private  EnemyAnimationsHandler _enemyAnimationsHandler;
    [SerializeField] private Collider _collider;
    public bool CanMove { get; private set; }
    
    public int Health { get; set; }
    public int Damage { get; set; }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        Debug.Log("Enemy Health = " + Health);
        
        if (Health <= 0)
        {
            CanMove = false;
            _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Die);
            _collider.enabled = false;
        }
    }

    public void SetCanMove(bool variable) => CanMove = variable;

    public Enemy(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    private void Awake()
    {
        CanMove = true;
    }

    public void Initialize(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }
}
