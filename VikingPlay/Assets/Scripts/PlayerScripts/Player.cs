using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private DamageDealer _playerDamageDealer;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private PlayerAudio _playerAudio;

    private HealthBar _healthBar;
    public GameObject GetFollowTarget() => _followTarget;
    public bool IsDead { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }

    public DamageDealer GetPlayerDamageDealer() => _playerDamageDealer;
    public static Player Instance { get; private set; }
    public bool CanMove { get; private set; } = true;

    public void SetCanMove(bool variable)
    {
        CanMove = variable;
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        
        _playerAnimationController.OnHit();
        
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            CanMove = false;
            _playerAnimationController.SetAnimation(PlayerAnimationController.TypesOfAnimation.Die);
            _playerCollider.enabled = false;
            _playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            GameManager.Instance.GameOver();
            _healthBar.SetHealth(0);
            return;
        }
        
        _healthBar.SetHealth(CurrentHealth);
    }

    public void AddHealth(int health)
    {
        _playerAudio.PlayHealthSound();
        
        if(CurrentHealth >= MaxHealth)
            return;

        CurrentHealth += health;
        _healthBar.SetHealth(CurrentHealth);
    }
    

    private void Awake()
    {
        Instance = this;
        CanMove = true;
    }

    public void Initialize(int health, int damage, HealthBar healthBar)
    {
        MaxHealth = health;
        CurrentHealth = health;
        Damage = damage;
        _healthBar = healthBar;
        _healthBar.SetMaxHealth(MaxHealth);
        _healthBar.SetHealth(CurrentHealth);
    }
    
}


