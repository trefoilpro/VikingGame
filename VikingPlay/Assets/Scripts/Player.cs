using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private DamageDealer _playerDamageDealer;
    public GameObject GetFollowTarget() => _followTarget;
    public int Health { get; set; }
    public int Damage { get; set; }

    public Player(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    public DamageDealer GetPlayerDamageDealer() => _playerDamageDealer;
    public static Player Instance { get; private set; }
    public bool CanMove { get; private set; } = true;

    public void SetCanMove(bool variable)
    {
        CanMove = variable;
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log("Player Health = " + Health);
    }
    

    private void Awake()
    {
        Instance = this;
        CanMove = true;
    }

    public void Initialize(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }
}


