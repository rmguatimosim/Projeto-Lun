

using System;
using EventArgs;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public event EventHandler<DamageEventArgs> OnDamage;
    public event EventHandler<HealEventArgs> OnHeal;



    public void Damage(int damage)
    {
        OnDamage?.Invoke(this, new DamageEventArgs
        {
            damage = damage
        });
    }
    
    public bool IsDead()
    {
        return health <= 0;
    }



}