using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController _movement;
    [SerializeField] private PlayerAudio _playerAudio;
    private Animator _animator;

    public enum TypesOfAnimation
    {
        Idle,
        Run,
        Attack,
        Die,
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation(TypesOfAnimation type)
    {
        switch (type)
        {
            case TypesOfAnimation.Idle:
                _animator.SetBool("IsRunning", false);
                break;
            case TypesOfAnimation.Run:
                _animator.SetBool("IsRunning", true);
                break;
            case TypesOfAnimation.Attack:
                _animator.SetTrigger("Attack");
                _animator.SetBool("IsRunning", false);
                break;
            case TypesOfAnimation.Die:
                _animator.SetTrigger("Die");
                _animator.SetBool("IsRunning", false);
                break;
        }
    }

    public void StartDealDamage()
    {
        Player.Instance.GetPlayerDamageDealer().StartDealingDamage();
        OnAttack();
    }
    
    public void EndDealDamage()
    {
        Player.Instance.GetPlayerDamageDealer().EndDealingDamage();
    }
    
    public void OnEndAttackAnimation()
    {
        Player.Instance.SetCanMove(true);
    }

    private void OnAnimatorMove()
    {
        Player.Instance.transform.position = _movement.NextPosition;
    }

    public void OnStep()
    {
        _playerAudio.PlayFootStepSound();
    }
    
    public void OnAttack()
    {
        _playerAudio.PlayAttackSound();
    }
    
    public void OnDie()
    {
        _playerAudio.PlayDieSound();
    }

    public void OnHit()
    {
        _playerAudio.PlayHitSound();
    }
}
