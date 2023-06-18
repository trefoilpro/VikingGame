using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private EnemyDamageDealer _enemyDamageDealer;
    [SerializeField] private Enemy _enemy;

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
                //_enemyAnimator.Play("KillPlayer");
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
}
