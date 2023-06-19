using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private float speed = 1f;
    
    [SerializeField] private int maxBottomAngle = 60;
    [SerializeField] private int maxTopAngle = 40;

    private NavMeshAgent _agent;
    private Vector2 _move;
    private Vector2 _look;
    private float fireValue;
    private Quaternion nextRotation;
    private float rotationPower = 0.25f;
    private float rotationLerp = 0.5f;
    public Vector3 NextPosition { get; private set; }


    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        fireValue = value.Get<float>();
    }

    public GameObject followTransform;

    private void Update()
    {
        if (!Player.Instance.CanMove)
        {
            NextPosition = transform.position;
            return;
        }
        
        if (fireValue > 0)
        {
            Attack();
            fireValue = 0;
            return;
        }

        
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;
        Debug.Log("Angle: " + angle);

        
        
        if (angle > 180 && angle < 360 - maxBottomAngle)
        {
            angles.x = 360 - maxBottomAngle;
        }
        else if (angle < 180 && angle > maxTopAngle)
        {
            angles.x = maxTopAngle;
        }


        followTransform.transform.localEulerAngles = angles;


        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation,
            Time.deltaTime * rotationLerp);

        nextRotation = new Quaternion(0, followTransform.transform.rotation.eulerAngles.y, 0, 0);
        if (_move.x == 0 && _move.y == 0)
        {
            playerAnimationController.SetAnimation(PlayerAnimationController.TypesOfAnimation.Idle);
            NextPosition = transform.position;
            
            return;
        }

        float moveSpeed = speed / 100f;
        Vector3 position = (_playerModel.transform.forward * Math.Abs(_move.y) * moveSpeed) +
                           (_playerModel.transform.forward * Math.Abs(_move.x) * moveSpeed);
        NextPosition = transform.position + position;
        
        playerAnimationController.SetAnimation(PlayerAnimationController.TypesOfAnimation.Run);


        Vector3 newAngles = Vector3.zero;

        float newAngleY = _move.y > 0 ? 0 : 180;

        float newAngleX = _move.x > 0 ? 1 : -1;

        switch (_move.x)
        {
            case > 0:
            case < 0:
            {
                if (_move.y == 0)
                {
                    newAngles.y += 90 * newAngleX;
                }
                else
                {
                    newAngles.y += (newAngleX * 90 + newAngleX * newAngleY) / 2;
                }

                break;
            }
            case 0:
                newAngles.y += newAngleY;
                break;
        }

        Quaternion newQuaternion = Quaternion.Euler(0, angles.y + newAngles.y, 0);

        _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, newQuaternion,
            Time.deltaTime * rotationLerp * 50);
    }

    private void Attack()
    {
        Player.Instance.SetCanMove(false);
        
        playerAnimationController.SetAnimation(PlayerAnimationController.TypesOfAnimation.Attack);
    }
}