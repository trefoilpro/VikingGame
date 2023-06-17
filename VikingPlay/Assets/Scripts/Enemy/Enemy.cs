using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    public int Health { get; set; }
    public int Damage { get; set; }
    
    public void TakeDamage(int damage)
    {
        
    }
    
    public Enemy(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }
}
