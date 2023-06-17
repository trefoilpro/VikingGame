using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    public bool CanMove { get; private set; }
    
    public int Health { get; set; }
    public int Damage { get; set; }

    public void TakeDamage(int damage)
    {
        
    }

    public void SetCanMove(bool variable) => CanMove = variable;

    public Enemy(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    private void Awake()
    {
        CanMove = true;
    }
}
