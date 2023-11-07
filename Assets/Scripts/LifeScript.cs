using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    
    public ChestOpenEvent onDead = new();
    

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

        if (IsDead())
        {
            onDead?.Invoke(gameObject);
        }
    }
    
    public void HealUp(GameObject healer, int healedHealth)
    {
        
        // Heal until it pass max health
        var newHealth = (float)(health + healedHealth);
        var clampedHealth = Mathf.Clamp(newHealth, 0f, (float)maxHealth);
        health = (int) clampedHealth;
        
        //Create effect
        CreateHealEffect();
        
        OnHeal?.Invoke(this, new HealEventArgs()
        {
            healer = healer,
            healedHealth = healedHealth
        });
    }

    public void RestoreHealth(GameObject healer)
    {
        // Heal
        health = maxHealth;
        
        //Create effect
        CreateHealEffect();
        
        OnHeal?.Invoke(this, new HealEventArgs()
        {
            healer = healer,
            healedHealth = maxHealth
        });
    }

    private void CreateHealEffect()
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
    
    [Serializable] public class ChestOpenEvent: UnityEvent<GameObject>{}


}
