using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private PlayerMover _playerMover;
    private NavMeshAgent _agent;
    public Vector2 _move;
    public Vector2 _look;
    public float aimValue;
    public float fireValue;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    public void OnAim(InputValue value)
    {
        aimValue = value.Get<float>();
    }


    public void OnFire(InputValue value)
    {
        fireValue = value.Get<float>();
    }

    public GameObject followTransform;

    private void Update()
    {
        if (!Player.Instance.CanMove)
        {
            nextPosition = transform.position;
            return;
        }
        
        if (fireValue > 0)
        {
            Attack();
            return;
        }

        Debug.Log("fireValue = " + fireValue);
        
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;


        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation,
            Time.deltaTime * rotationLerp);

        nextRotation = new Quaternion(0, followTransform.transform.rotation.eulerAngles.y, 0, 0);
        Debug.Log("_move.x = " + _move.x + " _move.y = " + _move.y);
        if (_move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;

            /*if (aimValue == 1)
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }*/

            return;
        }

        //движение
        float moveSpeed = speed / 100f;
        Vector3 position = (_playerModel.transform.forward * Math.Abs(_move.y) * moveSpeed) +
                           (_playerModel.transform.forward * Math.Abs(_move.x) * moveSpeed);
        nextPosition = transform.position + position;


        Vector3 newAngles = Vector3.zero;

        float newAngleX;
        float newAngleY;

        newAngleY = _move.y > 0 ? 0 : 180;

        newAngleX = _move.x > 0 ? 1 : -1;

        if (_move.x > 0 || _move.x < 0)
        {
            if (_move.y == 0)
            {
                newAngles.y += 90 * newAngleX;
            }
            else
            {
                newAngles.y += (newAngleX * 90 + newAngleX * newAngleY) / 2;
            }
        }
        else if (_move.x == 0)
        {
            newAngles.y += newAngleY;
        }


        Quaternion newQuaternion = Quaternion.Euler(0, angles.y + newAngles.y, 0);

        _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, newQuaternion,
            Time.deltaTime * rotationLerp * 50);

        //_playerModel.transform.localEulerAngles = new Vector3(0, angles.y + newAngles.y, 0);

        //followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private void Attack()
    {
        Player.Instance.SetCanMove(false);
        
        _playerMover.SetAnimation(PlayerMover.TypesOfAnimation.Attack);
    }
}