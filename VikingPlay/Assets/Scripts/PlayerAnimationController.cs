using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController _movement;
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
                break;
        }
    }

    public void StartDealDamage()
    {
        Player.Instance.GetPlayerDamageDealer().StartDealingDamage();
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
        Player.Instance.transform.position = _movement.nextPosition;
    }
    
    
}