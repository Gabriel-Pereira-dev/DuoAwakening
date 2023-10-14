using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using Unity.VisualScripting;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [HideInInspector] public event EventHandler<DamageEventArgs> OnDamage;
    [HideInInspector] public event EventHandler<HealEventArgs> OnHeal;
    public int maxHealth;
    // [HideInInspector] 
    public int health;
    public bool isVulnerable;
    
    public delegate bool CanInflictDamageDelegate(GameObject attacker, int damage);
    public CanInflictDamageDelegate canInflictDamageDelegate;
    

    public GameObject healingPrefab;
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
            
        // Can inflict damage?
        bool? canYouInflictDamage = canInflictDamageDelegate?.Invoke(attacker, damage);
        if (canYouInflictDamage.HasValue && !canYouInflictDamage.Value) return;
        
        // Inflict Damage
            health -= damage;
            OnDamage?.Invoke(this, new DamageEventArgs()
            {
                damage = damage,
                attacker = attacker
            });
        }
    }
    
    public void HealUp(GameObject healer, int healedHealth)
    {
        
        // Heal until it pass max health
        var newHealth = (float)(health + healedHealth);
        var clampedHealth = Mathf.Clamp(newHealth, 0f, (float)maxHealth);
        health = (int) clampedHealth;
        
        //Create effect
        createHealEffect();
        
        OnHeal?.Invoke(this, new HealEventArgs()
        {
            healer = healer,
            healedHealth = healedHealth
        });
    }

    public void RestoreHealth()
    {
        // Heal
        health = maxHealth;
        
        //Create effect
        createHealEffect();
    }

    private void createHealEffect()
    {
        //Create effect
        if (healingPrefab != null)
        {
            var effect = Instantiate(healingPrefab, transform.position, healingPrefab.transform.rotation);
            effect.transform.SetParent(transform);
            Destroy(effect, 5f);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }


}
