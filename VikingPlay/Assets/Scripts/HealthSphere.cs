using DG.Tweening;
using UnityEngine;

public class HealthSphere : MonoBehaviour
{
    private int _givingHealth = 1;
    private Sequence _sphereAnimation;
    
    private void Start()
    {
        _sphereAnimation = DOTween.Sequence();

        _sphereAnimation.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f));
        _sphereAnimation.Append(transform.DOMoveY(transform.position.y, 1f));
        _sphereAnimation.SetLoops(-1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player))
        {
            return;
        }
        
        _sphereAnimation.Kill();
        player.AddHealth(_givingHealth);
        Destroy(gameObject);
    }
}
