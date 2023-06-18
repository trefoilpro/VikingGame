using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _weaponLength;
    
    private bool _canDealDamage;
    private List<GameObject> _hasDealtDamage;

    
    private void Start()
    {
        _canDealDamage = false;
        _hasDealtDamage = new List<GameObject>();
    }

    private void Update()
    {
        if (_canDealDamage)
        {
            int layerMask = 1 << 8;
            Collider[] colliders = Physics.OverlapSphere(transform.position, _weaponLength, layerMask);
            
            foreach (Collider collider in colliders)
            {
                if (!_hasDealtDamage.Contains(collider.gameObject) && collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(Player.Instance.Damage);
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
