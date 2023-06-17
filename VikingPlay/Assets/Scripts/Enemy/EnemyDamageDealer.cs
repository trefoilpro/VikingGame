using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    private bool _canDealDamage;
    private List<GameObject> _hasDealtDamage;

    [SerializeField] private float _weaponLength;
    [SerializeField] private Enemy _enemy;
    private void Start()
    {
        _canDealDamage = false;
        _hasDealtDamage = new List<GameObject>();
    }

    private void Update()
    {
        if (_canDealDamage)
        {
            int layerMask = 1 << 6;
            Collider[] colliders = Physics.OverlapSphere(transform.position, _weaponLength, layerMask);
            
            foreach (Collider collider in colliders)
            {
                if (!_hasDealtDamage.Contains(collider.gameObject) && collider.gameObject.TryGetComponent(out Player player))
                {
                    player.TakeDamage(_enemy.Damage);
                    _hasDealtDamage.Add(collider.gameObject);
                    
                }
            }
        }
    }
    
    public void StartDealingDamage()
    {
        _canDealDamage = true;
        _hasDealtDamage.Clear();
    }
    
    public void EndDealingDamage()
    {
        _canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _weaponLength);
    }
}
