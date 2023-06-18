using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private DamageDealer _playerDamageDealer;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private Rigidbody _playerRigidbody;

    private PlayerHealthBar _playerHealthBar;
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
        
        

        Debug.Log("CurrentHealth: " + CurrentHealth);
        
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            CanMove = false;
            _playerAnimationController.SetAnimation(PlayerAnimationController.TypesOfAnimation.Die);
            _playerCollider.enabled = false;
            _playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            GameManager.Instance.GameOver();
            _playerHealthBar.SetHealth(0);
            return;
        }
        
        _playerHealthBar.SetHealth(CurrentHealth);
        
    }
    

    private void Awake()
    {
        Instance = this;
        CanMove = true;
    }

    public void Initialize(int health, int damage, PlayerHealthBar playerHealthBar)
    {
        MaxHealth = health;
        CurrentHealth = health;
        Damage = damage;
        _playerHealthBar = playerHealthBar;
        _playerHealthBar.SetMaxHealth(MaxHealth);
        _playerHealthBar.SetHealth(CurrentHealth);
    }
}


