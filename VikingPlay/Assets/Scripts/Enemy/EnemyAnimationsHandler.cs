using UnityEngine;

public class EnemyAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private EnemyDamageDealer _enemyDamageDealer;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyAudio _enemyAudio;

    public enum TypesOfAnimations
    {
        Idle,
        Walk,
        Run,
        Attack,
        Die,
    }

    private TypesOfAnimations _enemyTypeOfMoving = TypesOfAnimations.Idle;

    public void SetEnemyAnimation(TypesOfAnimations typesOfMoving)
    {
        if (typesOfMoving == _enemyTypeOfMoving)
            return;

        _enemyTypeOfMoving = typesOfMoving;

        switch (_enemyTypeOfMoving)
        {
            case TypesOfAnimations.Idle:
            {
                _enemyAnimator.SetBool("IsMoving", false);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfAnimations.Walk:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfAnimations.Run:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", true);
                break;
            }
            case TypesOfAnimations.Attack:
            {
                _enemyAnimator.SetTrigger("Attack");
                break;
            }
            case TypesOfAnimations.Die:
            {
                _enemyAnimator.SetTrigger("Die");
                break;
            }
        }
    }

    public void StartDealDamage()
    {
        _enemyDamageDealer.StartDealingDamage();
        OnAttack();
    }

    public void EndDealDamage()
    {
        _enemyDamageDealer.EndDealingDamage();
    }
    
    public void EndAnimationAttack()
    {
        _enemy.SetCanMove(true);
    }
    
    public void EndAnimationDie()
    {
        _enemy.GetHealthBar().gameObject.SetActive(false);
        _enemy.SpawnHealthSphere();
    }

    public void OnFootstep()
    {
        _enemyAudio.PlayFootStepSound();
    }
    
    public void OnHit()
    {
        _enemyAudio.PlayHitSound();
    }

    public void OnDie()
    {
        _enemyAudio.PlayDieSound();
    }
    
    public void OnAttack()
    {
        _enemyAudio.PlayAttackSound();
    }
}
