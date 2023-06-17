using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerController _movement;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        Player.Instance.transform.position = _movement.nextPosition;
        Debug.Log("OnAnimatorMove");
    }
}
