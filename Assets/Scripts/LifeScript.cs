using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [HideInInspector] public event EventHandler<DamageEventArgs> OnDamage;
    public int maxHealth;
    // [HideInInspector] 
    public int health;
    public bool isVulnerable;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        isVulnerable = true;
    }

    public void InflictDamage(GameObject attacker, int damage)
    {
        if (isVulnerable)
        {
            health -= damage;
            OnDamage?.Invoke(this, new DamageEventArgs()
            {
                damage = damage,
                attacker = attacker
            });
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }


}
